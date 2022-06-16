using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeChoice : MonoBehaviour
{
    public GroundBed groundBed;
    public Transform ContainerUpgrades;
    public Farm farm;
    public ChoiceUpgradeButton upgradeButton;
    public ChoisedPlantBuy choisedplant;
    public ChoiceUpgradeButton ChoisedUpgrade;
    public Upgrade upgrade1;
    public Upgrade upgrade2;

    public void SetUpgrades() {
        foreach (Transform child in ContainerUpgrades)
            Destroy(child.gameObject);
        for (int i=0; i < farm.AvailibleUpgrades.Count; i++) {
            if (farm.AvailibleUpgrades[i].Type == "GroundBed" && !groundBed.ExistUpgrades.Contains(farm.AvailibleUpgrades[i])) {
                if (groundBed.ExistUpgrades.Contains(upgrade1) && farm.AvailibleUpgrades[i] == upgrade2 || !groundBed.ExistUpgrades.Contains(upgrade1) && farm.AvailibleUpgrades[i] != upgrade2) {
                    var button = Instantiate(upgradeButton, ContainerUpgrades);
                    button.HaveUpgrade = farm.AvailibleUpgrades[i];
                    button.choice = this;
                    button.SetVizual();
                }
            }
        }
    }
}
