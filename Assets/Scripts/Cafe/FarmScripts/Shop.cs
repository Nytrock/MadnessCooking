using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Transform ContainerPrefab;
    public Transform ContainerContainers;
    public UpgradeCell UpgradePrefab;
    public List<Upgrade> UpgradesFarm;
    public List<GameObject> Containers;
    public int CountContainers;
    public int indexContainer;
    public Button Right;
    public Button Left;
    public Text CashDescription;
    public Text CashName;
    public Text CashCost;
    public Image CashImage;
    public GameObject BuyUpgrade;
    public MoneyBar money;
    public Upgrade ChoisedUpgrade;
    public Farm farm;
    public Button BuyButton;
    public List<Upgrade> TimerUpgrades;
    public int IndexTimerCar;
    public CarTimer timer;
    public List<Upgrade> CarUpgrades;
    public int IndexBiggerCar;
    public List<Upgrade> PourUpgrades;
    public int IndexPour;
    public Fountain WaterFountain;
    public List<Upgrade> FertilizeUpgrades;
    public int IndexFertilize;
    public List<Upgrade> BugsUpgrades;
    public int IndexBug;
    public ManureCreate manure;
    public Chickens chickens;
    public FlourCreate flour;
    public Fountain MilkCreate;

    public void Start()
    {
        UpdateUpgrades();
    }

    void Update()
    {
        for (int i = 0; i < CountContainers; i++) {
            if (i == indexContainer)
                Containers[i].SetActive(true);
            else
                Containers[i].SetActive(false);
        }

        if (ChoisedUpgrade != null) {
            if (money.Coins >= ChoisedUpgrade.Cost)
                BuyButton.interactable = true;
            else
                BuyButton.interactable = false;
        }
    }

    public void UpdateUpgrades()
    {
        if (UpgradesFarm.Count % 8 == 0)
            CountContainers = UpgradesFarm.Count / 8;
        else
            CountContainers = UpgradesFarm.Count / 8 + 1;
        foreach (Transform cont in ContainerContainers)
            Destroy(cont.gameObject);
        Containers.Clear();
        for (int i = 0; i < CountContainers; i++) {
            var container = Instantiate(ContainerPrefab, ContainerContainers);
            Containers.Add(container.gameObject);
            for (int j = 8 * i; j < 8 + 8 * i; j++) {
                if (j < UpgradesFarm.Count) {
                    var upgradeCell = Instantiate(UpgradePrefab, container);
                    if (UpgradesFarm[j] == TimerUpgrades[0]) {
                        upgradeCell.Cost.text = TimerUpgrades[IndexTimerCar].Cost.ToString();
                        upgradeCell.ImageUpgrade.sprite = TimerUpgrades[IndexTimerCar].SpriteUpgrade;
                        upgradeCell.Description = TimerUpgrades[IndexTimerCar].Description;
                        upgradeCell.Name = TimerUpgrades[IndexTimerCar].Name;
                        upgradeCell.upgrade = TimerUpgrades[IndexTimerCar];
                    } else if (UpgradesFarm[j] == CarUpgrades[0]) {
                        upgradeCell.Cost.text = CarUpgrades[IndexBiggerCar].Cost.ToString();
                        upgradeCell.ImageUpgrade.sprite = CarUpgrades[IndexBiggerCar].SpriteUpgrade;
                        upgradeCell.Description = CarUpgrades[IndexBiggerCar].Description;
                        upgradeCell.Name = CarUpgrades[IndexBiggerCar].Name;
                        upgradeCell.upgrade = CarUpgrades[IndexBiggerCar];
                    } else if (UpgradesFarm[j] == PourUpgrades[0]) {
                        upgradeCell.Cost.text = PourUpgrades[IndexPour].Cost.ToString();
                        upgradeCell.ImageUpgrade.sprite = PourUpgrades[IndexPour].SpriteUpgrade;
                        upgradeCell.Description = PourUpgrades[IndexPour].Description;
                        upgradeCell.Name = PourUpgrades[IndexPour].Name;
                        upgradeCell.upgrade = PourUpgrades[IndexPour];
                    } else if (UpgradesFarm[j] == FertilizeUpgrades[0]) {
                        upgradeCell.Cost.text = FertilizeUpgrades[IndexFertilize].Cost.ToString();
                        upgradeCell.ImageUpgrade.sprite = FertilizeUpgrades[IndexFertilize].SpriteUpgrade;
                        upgradeCell.Description = FertilizeUpgrades[IndexFertilize].Description;
                        upgradeCell.Name = FertilizeUpgrades[IndexFertilize].Name;
                        upgradeCell.upgrade = FertilizeUpgrades[IndexFertilize];
                    } else if (UpgradesFarm[j] == BugsUpgrades[0]) {
                        upgradeCell.Cost.text = BugsUpgrades[IndexBug].Cost.ToString();
                        upgradeCell.ImageUpgrade.sprite = BugsUpgrades[IndexBug].SpriteUpgrade;
                        upgradeCell.Description = BugsUpgrades[IndexBug].Description;
                        upgradeCell.Name = BugsUpgrades[IndexBug].Name;
                        upgradeCell.upgrade = BugsUpgrades[IndexBug];
                    } else {
                        upgradeCell.Cost.text = UpgradesFarm[j].Cost.ToString();
                        upgradeCell.ImageUpgrade.sprite = UpgradesFarm[j].SpriteUpgrade;
                        upgradeCell.Description = UpgradesFarm[j].Description;
                        upgradeCell.Name = UpgradesFarm[j].Name;
                        upgradeCell.upgrade = UpgradesFarm[j];
                    }
                    upgradeCell.shop = this;
                }
            }
        }
        UpdateButton();
    }

    public void LeftButton()
    {
        indexContainer -= 1;
        UpdateButton();
    }

    public void RightButton()
    {
        indexContainer += 1;
        UpdateButton();
    }

    public void UpdateButton()
    {
        if (indexContainer - 1 >= 0)
            Left.gameObject.SetActive(true);
        else
            Left.gameObject.SetActive(false);
        if (indexContainer + 1 < Containers.Count)
            Right.gameObject.SetActive(true);
        else
            Right.gameObject.SetActive(false);
    }

    public void Buy()
    {
        money.AddCoins(-ChoisedUpgrade.Cost);
        BuyUpgrade.SetActive(false);
        if (TimerUpgrades.Contains(ChoisedUpgrade)) { 
            IndexTimerCar += 1;
            if (IndexTimerCar == TimerUpgrades.Count)
                UpgradesFarm.Remove(TimerUpgrades[0]);
        } else if (CarUpgrades.Contains(ChoisedUpgrade)) {
            IndexBiggerCar += 1;
            if (IndexBiggerCar == CarUpgrades.Count)
                UpgradesFarm.Remove(CarUpgrades[0]);
        } else if (PourUpgrades.Contains(ChoisedUpgrade)) {
            IndexPour += 1;
            if (IndexPour == PourUpgrades.Count)
                UpgradesFarm.Remove(PourUpgrades[0]);
        } else if (FertilizeUpgrades.Contains(ChoisedUpgrade)) {
            IndexFertilize += 1;
            if (IndexFertilize == FertilizeUpgrades.Count)
                UpgradesFarm.Remove(FertilizeUpgrades[0]);
        } else if (BugsUpgrades.Contains(ChoisedUpgrade)) {
            IndexBug += 1;
            if (IndexBug == BugsUpgrades.Count)
                UpgradesFarm.Remove(BugsUpgrades[0]);
        }else {
            UpgradesFarm.Remove(ChoisedUpgrade);
        }
        farm.AvailibleUpgrades.Add(ChoisedUpgrade);
        timer.UpdateSpeed();
        WaterFountain.UpdateUpgrade();
        manure.UpdateUpgrade();
        chickens.UpdateUpgrade();
        flour.UpdateUpgrade();
        MilkCreate.UpdateUpgrade();
        UpdateUpgrades();
    }
}
