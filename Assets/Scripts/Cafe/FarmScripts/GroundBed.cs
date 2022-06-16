using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundBed : MonoBehaviour
{
    public Farm farm;
    public Animator PanelChoice;
    public ScrollRect Scroll;
    public OkChoicePlant ButtonOk;
    public Image plant;
    public Ingridient ingridient;
    public GameObject PanelPlant;
    public Image ImageInterface;
    public Text NameInterface;
    public Text DescriptionInterface;
    public Text KolInterface;
    public Slider PlantSlider;
    public bool StartPlant;
    public int KolIngridient;
    public FarmCar Car;
    public Fountain fountain;
    public Button fountainButton;
    public float TimePouring;
    public float TimeManuring;
    public float MultiplyPlantingBugs = 1;
    public float MultiplyPlantingWater = 1;
    public float MultiplyPlantingManure = 1;
    public GameObject BugsContainer;
    public Animator DestroyBugs;
    public Transform BugsDestroyContainer;
    public Animator PanelCar;
    public FlourCreate flour;
    public ManureCreate manure;
    public Button ManureButton;
    public Image Ground;
    public Sprite GroundSprite;
    public Sprite GroundNow;
    public List<Upgrade> ExistUpgrades;
    public Animator PanelUpgrades;
    public GameObject ButtonsMenu;
    public BugsContainer Bugs;
    public Image FertilizeImage;
    public Upgrade SborUpgrade;
    public Upgrade ManureUpgrade;
    public Upgrade PourUpgrade;
    public Upgrade BugsUpgrade1;
    public Upgrade BugsUpgrade2;
    public bool ActivePour;
    public bool ActiveManure;
    public AudioSource Get;
    public AudioSource Seed;
    public AudioSource Press;
    public AudioSource PourVol;
    public AudioSource FertilizeVol;
    public AudioSource BugsDestroyVol;

    void Start()
    {
        PlantSlider.interactable = false;
        UpdateUpgrades();
    }

    void Update()
    {
        if (StartPlant) {
            if (PlantSlider.value < PlantSlider.maxValue) {
                PlantSlider.value += Time.deltaTime * MultiplyPlantingWater * MultiplyPlantingBugs * MultiplyPlantingManure;
            } else {
                KolIngridient += 1;
                PlantSlider.value = 0;
                KolInterface.text = KolIngridient.ToString();
                if (ExistUpgrades.Contains(SborUpgrade))
                    IngridientsToCar();
            }
        }

        if (fountain.Count == 0)
            fountainButton.interactable = false;
        else
            fountainButton.interactable = true;

        if (manure.CountManure == 0)
            ManureButton.interactable = false;
        else
            ManureButton.interactable = true;

        if (TimePouring > 0 && !ActivePour)
            TimePouring -= Time.deltaTime;
        else if (!ActivePour) { 
            MultiplyPlantingWater = 1;
            Ground.sprite = GroundNow;
        }

        if (TimeManuring > 0 && !ActiveManure)
            TimeManuring -= Time.deltaTime;
        else if (!ActiveManure) {
            MultiplyPlantingManure = 1;
            FertilizeImage.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void PanelActive()
    {
        if (!PanelCar.GetBool("Active")) {
            Seed.Play();
            if (ingridient == null) {
                farm.FillChoicePlant();
                PanelChoice.SetBool("Active", true);
                ButtonsMenu.SetActive(!ButtonsMenu.activeSelf);
                Scroll.enabled = false;
                ButtonOk.ground = this;
            } else {
                PanelPlant.SetActive(!PanelPlant.activeSelf);
            }
        }
    }

    public void ActiveUpgradePanel()
    {
        Press.Play();
        PanelUpgrades.SetBool("Active", !PanelUpgrades.GetBool("Active"));
        ButtonsMenu.SetActive(!ButtonsMenu.activeSelf);
        if (PanelUpgrades.GetComponent<UpgradeChoice>().groundBed == null) {
            PanelUpgrades.GetComponent<UpgradeChoice>().groundBed = this;
            PanelUpgrades.GetComponent<UpgradeChoice>().SetUpgrades();
        } else
            PanelUpgrades.GetComponent<UpgradeChoice>().groundBed = null;
    }

    public void SpesialPanelActive()
    {
        Seed.Play();
        farm.FillChoicePlant();
        PanelChoice.SetBool("Active", true);
        ButtonsMenu.SetActive(!ButtonsMenu.activeSelf);
        Scroll.enabled = false;
        ButtonOk.ground = this;
    }

    public void IngridientsToCar()
    {
        if (KolIngridient > 0) {
            Get.Play();
            if (ingridient.Name == "Wheat") {
                flour.CountCorn += KolIngridient;
                flour.TextCorn.text = flour.CountCorn.ToString();
                KolIngridient = 0;
            } else {
                if (Car.IngridientsInCar.Contains(ingridient)) {
                    if (Car.Fullness + KolIngridient <= Car.Size || Car.Size == -1) {
                        Car.CountIngridientsCar[Car.IngridientsInCar.IndexOf(ingridient)] += KolIngridient;
                        Car.Fullness += KolIngridient;
                        KolIngridient = 0;
                    } else {
                        Car.CountIngridientsCar[Car.IngridientsInCar.IndexOf(ingridient)] += Car.Size - Car.Fullness;
                        KolIngridient -= Car.Size - Car.Fullness;
                        Car.Fullness = Car.Size;
                    }
                } else {
                    Car.IngridientsInCar.Add(ingridient);
                    if (Car.Fullness + KolIngridient <= Car.Size || Car.Size == -1) {
                        Car.CountIngridientsCar.Add(KolIngridient);
                        Car.Fullness += KolIngridient;
                        KolIngridient = 0;
                    } else {
                        Car.CountIngridientsCar.Add(Car.Size - Car.Fullness);
                        KolIngridient -= Car.Size - Car.Fullness;
                        Car.Fullness = Car.Size;
                    }
                }
                Car.TextFullness.text = Car.Fullness.ToString() + "/" + Car.Size.ToString();
            }
            KolInterface.text = "0";
            Car.UpdateIngridients();
        }
    }

    public void Pour()
    {
        PourVol.Play();
        fountain.Count -= 1;
        fountain.CountText.text = fountain.Count.ToString();
        if (TimePouring <= 0) {
            MultiplyPlantingWater = 1.1f;
            TimePouring = 10;
            Ground.sprite = GroundSprite;
        }
    }

    public void Fertilize()
    {
        FertilizeVol.Play();
        manure.CountManure -= 1;
        manure.TextManure.text = manure.CountManure.ToString();
        if (TimeManuring <= 0) {
            MultiplyPlantingManure = 1.1f;
            TimeManuring = 10;
            FertilizeImage.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void ActiveDestroyBugs()
    {
        Press.Play();
        if (NewOrContinue.Education) {
            BugsContainer.transform.GetChild(0).gameObject.SetActive(true);
            BugsContainer.transform.GetChild(1).gameObject.SetActive(true);
            BugsContainer.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (ExistUpgrades.Contains(BugsUpgrade1)) {
            MultiplyPlantingBugs = 1;
            for (int i = 0; i < BugsContainer.transform.childCount; i++) {
                BugsContainer.transform.GetChild(i).gameObject.SetActive(false);
            }
        } else { 
            for (int i=0; i < BugsDestroyContainer.childCount; i++) {
                BugsDestroyContainer.GetChild(i).gameObject.SetActive(BugsContainer.transform.GetChild(i).gameObject.activeSelf);
                BugsDestroyContainer.GetChild(i).GetComponent<ButtonDestroyBug>().NeedBug = BugsContainer.transform.GetChild(i).gameObject;
                BugsDestroyContainer.GetChild(i).GetComponent<ButtonDestroyBug>().OriginalContainer = BugsContainer;
            }
            DestroyBugs.SetBool("Active", true);
            ButtonsMenu.SetActive(false);
            BugsContainer.GetComponent<BugsContainer>().DestroyActive = true;
        }
    }

    public void SetInterface(Ingridient ingridient)
    {
        ImageInterface.sprite = ingridient.ImageIngridient;
        NameInterface.text = ingridient.Name;
        DescriptionInterface.text = ingridient.Description;
        PlantSlider.maxValue = ingridient.TimePlant;
        PlantSlider.gameObject.SetActive(true);
        StartPlant = true;
        KolInterface.text = "0";
        KolIngridient = 0;
        PlantSlider.value = 0;
        BugsContainer.SetActive(true);
    }

    public void UpdateUpgrades() {
        if (ExistUpgrades.Contains(PourUpgrade)) {
            TimePouring = 1;
            ActivePour = true;
            Ground.sprite = GroundSprite;
            MultiplyPlantingWater = 1.1f;
        }
        if (ExistUpgrades.Contains(ManureUpgrade)) {
            TimeManuring = 1;
            ActiveManure = true;
            FertilizeImage.color = new Color(1f, 1f, 1f, 1f);
            MultiplyPlantingManure = 1.1f;
        }
        if (ExistUpgrades.Contains(BugsUpgrade2)) {
            BugsContainer.GetComponent<BugsContainer>().HaveUpdate = true;
            for (int i = 0; i < BugsContainer.transform.childCount; i++) {
                BugsContainer.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
