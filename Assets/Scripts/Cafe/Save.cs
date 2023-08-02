using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;

public class Save : MonoBehaviour
{
    public string savePath;
    public static Save instance;

    public List<Upgrade> AllUpgrades;
    public List<Ingridient> AllIngridients;
    public List<Food> AllFoods;
    public List<TechnicTemplate> AllTechnic;

    public Cafe OriginCafe;
    public ClockAnimator Clock;
    public ForOrders Orders;
    public ForIngridients Ingridients;
    public Farm OriginalFarm;
    public AddGardenBed AddBed;
    public Fountain fountain;
    public FarmCar car;
    public Shop FarmShop;
    public Fountain MilkCreate;
    public FlourCreate Flour;
    public Fridge fridge;
    public Chickens EggsCreate;
    public ManureCreate Manure;
    public InternetIngridientsFill ingridientsFill;
    public InternetFoodFill foodFill;
    public InternetTechnicFill technicFill;
    public InternetUpgradeShop upgradeFill;
    public SpawnClients spawnClients;

    [Serializable]
    public class CafeSave
    {
        public int Coins;
        public float FoodMultiply;
        public float SpeedTechnic;
        public TimeSpan Time;
        public int CountPositions;
        public List<int> UpgradesNumber = new List<int>();
        public List<int> FoodNumber = new List<int>();
        public bool Education;
    }

    [Serializable]
    public class KitchenSave
    {
        public List<int> ActiveTechnicTemplate = new List<int>();
        public List<int> IndexIngridients = new List<int>();
        public List<int> NumberIngridients = new List<int>();
        public List<int> Stregth = new List<int>();
    }

    [Serializable]
    public class FarmSave
    {
        public List<int> IndexIngridients = new List<int>();
        public List<int> CountIngridients = new List<int>();
        public List<float> TimesPouring = new List<float>();
        public List<float> TimesManuring = new List<float>();
        public List<List<bool>> CountsBugs = new List<List<bool>>();
        public List<List<int>> ExistsUpgrades = new List<List<int>>();
        public List<int> IndexBugs = new List<int>();
        public int CountBeds;
        public int CountWater;
        public bool CarActive;
        public float TimerCar;
        public bool Driving;
        public List<int> CarIngridients = new List<int>();
        public List<int> CarCountIngridients = new List<int>();
        public List<int> ShopUpgrades = new List<int>();
        public List<int> FarmIngridients = new List<int>();
        public List<int> FarmUpgrades = new List<int>();
        public int CountMilk;
        public int CountFlour;
        public int CountCorn;
        public int CountEggs;
        public int CountShit;
        public int CountManure;
        public int IndexTimerCar;
        public int IndexPour;
        public int IndexFertilize;
        public int IndexBug;
    }

    [Serializable]
    public class OfficeSave
    {
        public List<int> ShopIngridients = new List<int>();
        public List<int> ShopFood = new List<int>();
        public List<int> ShopTechnic = new List<int>();
        public List<int> ShopUpgrades = new List<int>();
        public int IndexCafe;
        public int IndexKitchen;
        public int IndexInternet;
    }

    private void Start()
    {
        instance = this;
    }

    public void SaveAll()
    {
        PanelSave.SetBool("Active", true);
        CafeSave cafe = new();
        cafe.Coins = OriginCafe.money.Coins;
        cafe.Time = Clock.timespan;
        for (int i = 0; i < OriginCafe.AvailableUpgrades.Count; i++) {
            cafe.UpgradesNumber.Add(AllUpgrades.IndexOf(OriginCafe.AvailableUpgrades[i]));
        }
        for (int i = 0; i < OriginCafe.AvailableFood.Count; i++) {
            cafe.FoodNumber.Add(AllFoods.IndexOf(OriginCafe.AvailableFood[i]));
        }
        cafe.CountPositions = OriginCafe.CountSits;
        cafe.FoodMultiply = spawnClients.FoodMultiply;
        cafe.SpeedTechnic = OriginCafe.SpeedTechnic;
        cafe.Education = NewOrContinue.Education;


        KitchenSave kitchen = new();
        kitchen.NumberIngridients = Ingridients.NumberIngridients;
        for (int i = 0; i < Orders.AvailableTechnicTemplate.Count; i++) {
            kitchen.ActiveTechnicTemplate.Add(AllTechnic.IndexOf(Orders.AvailableTechnicTemplate[i]));
        }
        for (int i = 0; i < Ingridients.HaveIngridients.Count; i++) {
            kitchen.IndexIngridients.Add(AllIngridients.IndexOf(Ingridients.HaveIngridients[i]));
        }
        for (int i=0; i < AllTechnic.Count; i++) {
            kitchen.Stregth.Add(AllTechnic[i].strength);
        }


        FarmSave farm = new();
        for (int i = 0; i < OriginalFarm.GroundBeds.Count; i++) {
            if (OriginalFarm.GroundBeds[i].ingridient == null) {
                farm.IndexIngridients.Add(-1);
                farm.CountIngridients.Add(-1);
                farm.TimesPouring.Add(-1);
                farm.TimesManuring.Add(-1);
                farm.CountsBugs.Add(new List<bool>());
                farm.ExistsUpgrades.Add(new List<int>());
                farm.IndexBugs.Add(-1);
            } else {
                farm.IndexIngridients.Add(AllIngridients.IndexOf(OriginalFarm.GroundBeds[i].ingridient));
                farm.CountIngridients.Add(OriginalFarm.GroundBeds[i].KolIngridient);
                farm.TimesPouring.Add(OriginalFarm.GroundBeds[i].TimePouring);
                farm.TimesManuring.Add(OriginalFarm.GroundBeds[i].TimeManuring);
                List<bool> vs = new List<bool>();
                for (int j = 0; j < OriginalFarm.GroundBeds[i].Bugs.Count; j++) {
                    vs.Add(OriginalFarm.GroundBeds[i].Bugs.gameObject.transform.GetChild(j).gameObject.activeSelf);
                }
                farm.CountsBugs.Add(vs);
                farm.IndexBugs.Add(OriginalFarm.GroundBeds[i].Bugs.IndexBug);
                List<int> sus = new();
                for (int j = 0; j < OriginalFarm.GroundBeds[i].ExistUpgrades.Count; j++) {
                    sus.Add(AllUpgrades.IndexOf(OriginalFarm.GroundBeds[i].ExistUpgrades[j]));
                }
                farm.ExistsUpgrades.Add(sus);
            }
        }
        farm.CountBeds = AddBed.Index;
        farm.CountWater = fountain.Count;
        farm.CarActive = car.GetComponent<Animator>().GetBool("ToKitchen");
        farm.TimerCar = car.timer.NowTime;
        farm.Driving = car.Driving;
        for (int i = 0; i < car.IngridientsInCar.Count; i++) {
            farm.CarIngridients.Add(AllIngridients.IndexOf(car.IngridientsInCar[i]));
        }
        for (int i = 0; i < car.CountIngridientsCar.Count; i++) {
            farm.CarCountIngridients.Add(car.CountIngridientsCar[i]);
        }
        for (int i = 0; i < FarmShop.UpgradesFarm.Count; i++) {
            farm.ShopUpgrades.Add(AllUpgrades.IndexOf(FarmShop.UpgradesFarm[i]));
        }
        for (int i = 0; i < OriginalFarm.AvailibleIngridients.Count; i++){
            farm.FarmIngridients.Add(AllIngridients.IndexOf(OriginalFarm.AvailibleIngridients[i]));
        }
        for (int i=0; i < OriginalFarm.AvailibleUpgrades.Count; i++) {
            farm.FarmUpgrades.Add(AllUpgrades.IndexOf(OriginalFarm.AvailibleUpgrades[i]));
        }
        farm.CountMilk = fridge.CountMilk;
        farm.CountCorn = Flour.CountCorn;
        farm.CountFlour = fridge.CountFlour;
        farm.CountEggs = EggsCreate.KolIngridient;
        farm.CountShit = Manure.CountShit;
        farm.CountManure = Manure.CountManure;
        farm.IndexTimerCar = FarmShop.IndexTimerCar;
        farm.IndexPour = FarmShop.IndexPour;
        farm.IndexFertilize = FarmShop.IndexFertilize;
        farm.IndexBug = FarmShop.IndexBug;


        OfficeSave office = new();
        for (int i=0; i < ingridientsFill.AllIngridient.Count; i++){
            office.ShopIngridients.Add(AllIngridients.IndexOf(ingridientsFill.AllIngridient[i]));
        }
        for (int i=0; i < foodFill.AllFood.Count; i++) {
            office.ShopFood.Add(AllFoods.IndexOf(foodFill.AllFood[i]));
        }
        for (int i=0; i < technicFill.AllTechicTemplate.Count; i++) {
            office.ShopTechnic.Add(AllTechnic.IndexOf(technicFill.AllTechicTemplate[i]));
        }
        for (int i=0; i < upgradeFill.AllUpgrades.Count; i++) {
            office.ShopUpgrades.Add(AllUpgrades.IndexOf(upgradeFill.AllUpgrades[i]));
        }
        office.IndexCafe = upgradeFill.IndexCafe;
        office.IndexKitchen = upgradeFill.IndexKitchen;
        office.IndexInternet = upgradeFill.IndexInternet;
        if (!Directory.Exists(Application.dataPath + "/save")){
            Directory.CreateDirectory(Application.dataPath + "/save");
        }

        FileStream stream = new(Application.dataPath + savePath, FileMode.Create);
        BinaryFormatter form = new();
        form.Serialize(stream, cafe);
        form.Serialize(stream, kitchen);
        form.Serialize(stream, farm);
        form.Serialize(stream, office);
        stream.Close();
        PanelSave.SetBool("Active", false);
    }

    public void Load()
    {
        if (!File.Exists(Application.dataPath + savePath))
            return;

        FileStream stream = new(Application.dataPath + savePath, FileMode.Open);
        BinaryFormatter form = new();
        try
        {
            CafeSave cafe = (CafeSave)form.Deserialize(stream);
            OriginCafe.money.Coins = cafe.Coins;
            Clock.timespan = cafe.Time;
            Clock.SkyChange();
            OriginCafe.AvailableUpgrades.Clear();
            for (int i = 0; i < cafe.UpgradesNumber.Count; i++) {
                OriginCafe.AvailableUpgrades.Add(AllUpgrades[cafe.UpgradesNumber[i]]);
            }
            OriginCafe.AvailableFood.Clear();
            for (int i = 0; i < cafe.FoodNumber.Count; i++) {
                OriginCafe.AvailableFood.Add(AllFoods[cafe.FoodNumber[i]]);
            }
            OriginCafe.CountSits = cafe.CountPositions;
            spawnClients.FoodMultiply = cafe.FoodMultiply;
            OriginCafe.SpeedTechnic = cafe.SpeedTechnic;
            NewOrContinue.Education = cafe.Education;
            OriginCafe.UpdateContainers();
            OriginCafe.UpdateDecor();


            KitchenSave kitchen = (KitchenSave)form.Deserialize(stream);
            for (int i = 0; i < kitchen.ActiveTechnicTemplate.Count; i++) {
                Orders.AvailableTechnicTemplate.Add(AllTechnic[kitchen.ActiveTechnicTemplate[i]]);
                Orders.AvailableTechnic.Add(AllTechnic[kitchen.ActiveTechnicTemplate[i]].technic);
                AllTechnic[kitchen.ActiveTechnicTemplate[i]].gameObject.SetActive(true);
            }
            Ingridients.NumberIngridients.Clear();
            Ingridients.HaveIngridients.Clear();
            for (int i = 0; i < kitchen.IndexIngridients.Count; i++) {
                Ingridients.NumberIngridients.Add(kitchen.NumberIngridients[i]);
                Ingridients.HaveIngridients.Add(AllIngridients[kitchen.IndexIngridients[i]]);
            }
            for (int i=0; i < kitchen.Stregth.Count; i++) {
                AllTechnic[i].strength = kitchen.Stregth[i];
            }
            Ingridients.RenderIngridients();


            FarmSave farm = (FarmSave)form.Deserialize(stream);
            for (int i = 0; i < OriginalFarm.GroundBeds.Count; i++) {
                if (farm.IndexIngridients[i] != -1) {
                    OriginalFarm.GroundBeds[i].ingridient = AllIngridients[farm.IndexIngridients[i]];
                    OriginalFarm.GroundBeds[i].plant.sprite = OriginalFarm.GroundBeds[i].ingridient.ImagePlant;
                    OriginalFarm.GroundBeds[i].plant.color = new Color(1f, 1f, 1f, 1f);
                    OriginalFarm.GroundBeds[i].KolIngridient = farm.CountIngridients[i];
                    OriginalFarm.GroundBeds[i].TimePouring = farm.TimesPouring[i];
                    OriginalFarm.GroundBeds[i].TimeManuring = farm.TimesManuring[i];
                    for (int j = 0; j < farm.CountsBugs[i].Count; j++) 
                        OriginalFarm.GroundBeds[i].Bugs.gameObject.transform.GetChild(j).gameObject.SetActive(farm.CountsBugs[i][j]);
                    OriginalFarm.GroundBeds[i].Bugs.IndexBug = farm.IndexBugs[i];
                    OriginalFarm.GroundBeds[i].SetInterface(OriginalFarm.GroundBeds[i].ingridient);
                    for(int j = 0; j < farm.ExistsUpgrades[i].Count; j++) {
                        OriginalFarm.GroundBeds[i].ExistUpgrades.Add(AllUpgrades[farm.ExistsUpgrades[i][j]]);
                    } 
                } else {
                    OriginalFarm.GroundBeds[i].ingridient = null;
                    OriginalFarm.GroundBeds[i].PlantSlider.gameObject.SetActive(false);
                    OriginalFarm.GroundBeds[i].BugsContainer.SetActive(false);
                    OriginalFarm.GroundBeds[i].plant.color = new Color(1f, 1f, 1f, 0f);
                    OriginalFarm.GroundBeds[i].KolIngridient = 0;
                    OriginalFarm.GroundBeds[i].TimePouring = 0;
                    OriginalFarm.GroundBeds[i].TimeManuring = 0;
                    OriginalFarm.GroundBeds[i].StartPlant = false;
                    OriginalFarm.GroundBeds[i].Bugs.IndexBug = -2;
                }
            }
            for (int i= 0; i < AddBed.Beds.Count; i++) {
                if (i < farm.CountBeds)
                    AddBed.Beds[i].SetActive(true);
                else
                    AddBed.Beds[i].SetActive(false);
            }
            AddBed.Index = farm.CountBeds;
            fountain.Count = farm.CountWater;
            fountain.CountText.text = fountain.Count.ToString();
            car.GetComponent<Animator>().SetBool("ToKitchen", farm.CarActive);
            car.timer.NowTime = farm.TimerCar;
            car.Driving = farm.Driving;
            car.IngridientsInCar.Clear();
            for (int i=0; i < farm.CarIngridients.Count; i++) {
                car.IngridientsInCar.Add(AllIngridients[farm.CarIngridients[i]]);
            }
            car.CountIngridientsCar.Clear();
            for (int i=0; i < farm.CarCountIngridients.Count; i++) {
                car.CountIngridientsCar.Add(farm.CarCountIngridients[i]);
            }
            car.UpdateIngridients();
            FarmShop.UpgradesFarm.Clear();
            for (int i = 0; i < farm.ShopUpgrades.Count; i++) {
                FarmShop.UpgradesFarm.Add(AllUpgrades[farm.ShopUpgrades[i]]);
            }
            FarmShop.UpdateUpgrades();
            OriginalFarm.AvailibleIngridients.Clear();
            for (int i=0; i < farm.FarmIngridients.Count; i++) {
                OriginalFarm.AvailibleIngridients.Add(AllIngridients[farm.FarmIngridients[i]]);
            }
            OriginalFarm.AvailibleUpgrades.Clear();
            for (int i=0; i < farm.FarmUpgrades.Count; i++) {
                OriginalFarm.AvailibleUpgrades.Add(AllUpgrades[farm.FarmUpgrades[i]]);
            }
            fridge.CountMilk = farm.CountMilk;
            MilkCreate.CountText.text = farm.CountMilk.ToString();
            Flour.CountCorn = farm.CountCorn;
            fridge.CountFlour = farm.CountFlour;
            EggsCreate.KolIngridient = farm.CountEggs;
            Manure.CountShit = farm.CountShit;
            Manure.CountManure = farm.CountManure;
            FarmShop.IndexTimerCar = farm.IndexTimerCar;
            FarmShop.IndexPour = farm.IndexPour;
            FarmShop.IndexFertilize = farm.IndexFertilize;
            FarmShop.IndexBug = farm.IndexBug;


            OfficeSave office = (OfficeSave)form.Deserialize(stream);
            ingridientsFill.AllIngridient.Clear();
            for (int i=0; i < office.ShopIngridients.Count; i++) {
                ingridientsFill.AllIngridient.Add(AllIngridients[office.ShopIngridients[i]]);
            }
            ingridientsFill.UpdateIngridients();
            foodFill.AllFood.Clear();
            for (int i = 0; i < office.ShopFood.Count; i++)  {
                foodFill.AllFood.Add(AllFoods[office.ShopFood[i]]);
            }
            foodFill.UpdateFood();
            technicFill.AllTechicTemplate.Clear();
            for (int i = 0; i < office.ShopTechnic.Count; i++) {
                technicFill.AllTechicTemplate.Add(AllTechnic[office.ShopTechnic[i]]);
            }
            technicFill.UpdateTechnic();
            upgradeFill.AllUpgrades.Clear();
            for(int i=0; i < office.ShopUpgrades.Count; i++) {
                upgradeFill.AllUpgrades.Add(AllUpgrades[office.ShopUpgrades[i]]);
            }
            upgradeFill.IndexCafe = office.IndexCafe;
            upgradeFill.IndexKitchen = office.IndexKitchen;
            upgradeFill.IndexInternet = office.IndexInternet;
            upgradeFill.UpdateUpgrade();
        } finally {
            stream.Close();
        }
    }

    public float TimeSave;
    public float CurrentTime;
    public Animator PanelSave;

    void Update()
    {
        if (CurrentTime < TimeSave)
            CurrentTime += Time.deltaTime;
        else {
            SaveAll();
            CurrentTime = 0;
        }
    }

    public void Awake()
    {
        if (NewOrContinue.Continue)
            Load();
        else
            SaveAll();
    }
}
