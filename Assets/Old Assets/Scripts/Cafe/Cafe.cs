using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cafe : MonoBehaviour
{
    public MoneyBar money;
    public List<Transform> PositionsClients;
    public List<bool> BusyPositions;
    public List<ContainerPosition> TwoSitsContainer;
    public List<Food> AvailableFood;
    public List<Upgrade> AvailableUpgrades;
    public List<Client> AvailableClients;
    public ForOrders orders;
    public ForIngridients ingridients;
    public int CountSits;
    public Transform ClientsContainer;
    public Save save;
    public Loading load;
    public float SpeedTechnic;
    public InternetUpgradeShop internetUpgrade;
    public Transform Camera;
    public Education education;

    public void Start()
    {
        UpdateUpgrades();
        UpdateDecor();
    }

    public void UpdateContainers()
    {
        PositionsClients.Clear();
        BusyPositions.Clear();
        for (int i = 1; i < ClientsContainer.childCount; i++)
            Destroy(ClientsContainer.GetChild(i).gameObject);
        for (int i = 0; i < TwoSitsContainer.Count; i++) {
            TwoSitsContainer[i].gameObject.SetActive(false);
        }
        for (int i=0; i < CountSits; i++) {
            TwoSitsContainer[i].gameObject.SetActive(true);
            PositionsClients.Add(TwoSitsContainer[i].Position1);
            PositionsClients.Add(TwoSitsContainer[i].Position2);
            BusyPositions.Add(false);
            BusyPositions.Add(false);
        }
    }

    public void Closed()
    {
        for(int i= 0; i < AvailableClients.Count; i++) {
            if (AvailableClients[i] != null)
                AvailableClients[i].yesNo.Click();
        }
    }

    public void UpdateUpgrades()
    {
        for (int i = 0; i < save.AllTechnic.Count; i++)
            save.AllTechnic[i].FasterSpeed = SpeedTechnic;
        load.MinFloat = 6;
        load.MaxFloat = 9;
        if (internetUpgrade.IndexInternet != 0){
            for (int i = 0; i < internetUpgrade.IndexInternet; i++) {
                load.MinFloat -= 2;
                load.MaxFloat -= 3;
            }
        }
    }

    public List<Upgrade> DecorKichenUpgrades;
    public List<GameObject> DecorKichenImages;

    public void UpdateDecor()
    {
        if (AvailableUpgrades.Contains(DecorKichenUpgrades[0])) {
            DecorKichenImages[0].SetActive(true);
            DecorKichenImages[1].SetActive(true);
            DecorKichenImages[2].SetActive(true);
            DecorKichenImages[3].SetActive(true);
        }
        if (AvailableUpgrades.Contains(DecorKichenUpgrades[1])) {
            DecorKichenImages[4].SetActive(true);
        }
        if (AvailableUpgrades.Contains(DecorKichenUpgrades[2])) {
            DecorKichenImages[5].SetActive(true);
        }
        if (AvailableUpgrades.Contains(DecorKichenUpgrades[3])) {
            DecorKichenImages[6].SetActive(true);
        }
        if (AvailableUpgrades.Contains(DecorKichenUpgrades[4])) {
            DecorKichenImages[7].SetActive(true);
        }
        if (AvailableUpgrades.Contains(DecorKichenUpgrades[5])){
            DecorKichenImages[8].SetActive(true);
        }
        if (AvailableUpgrades.Contains(DecorKichenUpgrades[6])) {
            DecorKichenImages[9].SetActive(true);
        }
        if (AvailableUpgrades.Contains(DecorKichenUpgrades[7])) {
            DecorKichenImages[10].SetActive(true);
        }
        if (AvailableUpgrades.Contains(DecorKichenUpgrades[8])) {
            DecorKichenImages[11].SetActive(true);
        }
        if (AvailableUpgrades.Contains(DecorKichenUpgrades[9])) {
            DecorKichenImages[12].SetActive(true);
        }
    }

}
