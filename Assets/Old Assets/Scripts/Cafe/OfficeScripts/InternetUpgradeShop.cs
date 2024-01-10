using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetUpgradeShop : MonoBehaviour
{
    public Transform ContainerContainers;
    public Transform Container;
    public InternetProduct product;
    public List<Upgrade> AllUpgrades;
    public List<GameObject> Containers;
    public List<int> CostsCafe;
    public int IndexCafe = 2;
    public Cafe cafe;
    public MoneyBar Money;
    public int indexContainer;
    public Button Left;
    public Button Right;

    public List<int> CostsTechnicSpeed;
    public List<Sprite> SpritesTechnicSpeed;
    public int IndexKitchen;

    public List<int> CostsInternet;
    public List<Sprite> SpritesInternet;
    public int IndexInternet;

    public AudioSource Press;

    void Start()
    {
        UpdateUpgrade();
    }

    void Update()
    {
        for (int i = 0; i < Containers.Count; i++)
        {
            if (i == indexContainer)
                Containers[i].SetActive(true);
            else
                Containers[i].SetActive(false);
        }
    }

    public void UpdateUpgrade()
    {
        int CountContainers;
        if (AllUpgrades.Count % 8 == 0)
            CountContainers = AllUpgrades.Count / 8;
        else
            CountContainers = AllUpgrades.Count / 8 + 1;
        foreach (Transform cont in ContainerContainers)
            Destroy(cont.gameObject);
        Containers.Clear();
        for (int i = 0; i < CountContainers; i++) {
            var container = Instantiate(Container, ContainerContainers);
            Containers.Add(container.gameObject);
            for (int j = 8 * i; j < 8 + 8 * i; j++){
                if (j < AllUpgrades.Count) {
                    var upgradeCell = Instantiate(product, container);
                    upgradeCell.NameProduct.Localize(AllUpgrades[j].Name);
                    if (AllUpgrades[j].Type == "Kitchen")
                        upgradeCell.ImageProduct.sprite = SpritesTechnicSpeed[IndexKitchen];
                    else if (AllUpgrades[j].Type == "Internet")
                        upgradeCell.ImageProduct.sprite = SpritesInternet[IndexInternet];
                    else
                        upgradeCell.ImageProduct.sprite = AllUpgrades[j].SpriteUpgrade;

                    upgradeCell.DescriptionProduct.Localize(AllUpgrades[j].Description);

                    if (AllUpgrades[j].Type == "Cafe")
                        upgradeCell.CostProduct.Localize(CostsCafe[IndexCafe].ToString());
                    else if (AllUpgrades[j].Type == "Kitchen")
                        upgradeCell.CostProduct.Localize(CostsTechnicSpeed[IndexKitchen].ToString());
                    else if (AllUpgrades[j].Type == "Internet")
                        upgradeCell.CostProduct.Localize(CostsInternet[IndexInternet].ToString());
                    else
                        upgradeCell.CostProduct.Localize(AllUpgrades[j].Cost.ToString());
                    upgradeCell.UpgradeFill = this;
                    upgradeCell.upgrade = AllUpgrades[j];
                } else {
                    break;
                }
            }
        }
        if (indexContainer >= Containers.Count)
            indexContainer -= 1;
        UpdateButton();
    }

    public void BuyUpgrade(Upgrade upgrade )
    {
        Press.Play();
        if (upgrade.Type == "Cafe") {
            Money.AddCoins(-CostsCafe[IndexCafe]);
            cafe.PositionsClients.Add(cafe.TwoSitsContainer[IndexCafe].Position1);
            cafe.PositionsClients.Add(cafe.TwoSitsContainer[IndexCafe].Position2);
            cafe.BusyPositions.Add(false);
            cafe.BusyPositions.Add(false);
            cafe.AvailableClients.Add(null);
            cafe.AvailableClients.Add(null);
            cafe.TwoSitsContainer[IndexCafe].gameObject.SetActive(true);
            if (IndexCafe + 1 < CostsCafe.Count)
                IndexCafe += 1;
            else
                AllUpgrades.Remove(upgrade);
            cafe.CountSits += 1;
        } else if (upgrade.Type == "Kitchen") {
            Money.AddCoins(-CostsTechnicSpeed[IndexKitchen]);
            cafe.SpeedTechnic += 0.1f;
            if (IndexKitchen + 1 < CostsTechnicSpeed.Count)
                IndexKitchen += 1;
            else
                AllUpgrades.Remove(upgrade);
        } else if (upgrade.Type == "Internet") {
            Money.AddCoins(-CostsInternet[IndexInternet]);
            IndexInternet += 1;
            if (IndexInternet >= SpritesInternet.Count)
                AllUpgrades.Remove(upgrade);
        } else {
            Money.AddCoins(-upgrade.Cost);
            cafe.AvailableUpgrades.Add(upgrade);
            AllUpgrades.Remove(upgrade);
        }
        UpdateUpgrade();
        cafe.UpdateUpgrades();
        cafe.UpdateDecor();
    }

    public void LeftButton()
    {
        indexContainer -= 1;
        Press.Play();
        UpdateButton();
    }

    public void RightButton()
    {
        indexContainer += 1;
        Press.Play();
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
}
