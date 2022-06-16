using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chickens : MonoBehaviour
{
    public GameObject PanelPlant;
    public Slider PlantSlider;
    public int KolIngridient;
    public Text KolInterface;
    public float MultiplyPlantingCorn = 1;
    public float TimePouring;
    public FarmCar Car;
    public Ingridient ingridient;
    public Upgrade AutoUpgrade;
    public Upgrade NeedUpgrade;
    public Farm farm;
    public bool AutoKorm;
    public AudioSource Get;
    public AudioSource Corn;

    void Start()
    {
        UpdateUpgrade();
    }

    public void Update()
    {
        KolInterface.text = KolIngridient.ToString();
        if (PlantSlider.value < PlantSlider.maxValue) {
            PlantSlider.value += Time.deltaTime * MultiplyPlantingCorn;
        } else {
            KolIngridient += 1;
            PlantSlider.value = 0;
            if (farm.AvailibleUpgrades.Contains(AutoUpgrade))
                IngridientsToCar();
        }

        if (TimePouring > 0)
            TimePouring -= Time.deltaTime;
        else if (!AutoKorm)
            MultiplyPlantingCorn = 1;
        else
            MultiplyPlantingCorn = 1.1f;
    }

    public void ActivePanel()
    {
        PanelPlant.SetActive(!PanelPlant.activeSelf);
    }

    public void MoreMultiply()
    {
        MultiplyPlantingCorn = 1.1f;
        TimePouring = 60;
        Corn.Play();
    }

    public void IngridientsToCar()
    {
        if (KolIngridient > 0)  {
            Get.Play();
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
                    Debug.Log(KolIngridient - Car.Size - Car.Fullness);
                    KolIngridient -= Car.Size - Car.Fullness;
                    Car.Fullness = Car.Size;
                }
            }
            Car.TextFullness.text = Car.Fullness.ToString() + "/" + Car.Size.ToString();
            Car.UpdateIngridients();
        }
    }
    
    public void UpdateUpgrade()
    {
        if (farm.AvailibleUpgrades.Contains(NeedUpgrade)) {
            AutoKorm = true;
        }
    }
}
