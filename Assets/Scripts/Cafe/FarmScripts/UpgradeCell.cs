using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCell : MonoBehaviour
{
    public Image ImageUpgrade;
    public Text Cost;
    public Shop shop;
    public string Description;
    public string Name;
    public Upgrade upgrade;
    public AudioSource Choise;

    public void Pressed()
    {
        Choise.Play();
        if (!shop.BuyUpgrade.activeSelf || shop.CashImage.sprite != ImageUpgrade.sprite) {
            shop.BuyUpgrade.SetActive(true);
            shop.CashDescription.text = Description;
            shop.CashName.text = Name;
            shop.CashCost.text = Cost.text;
            shop.CashImage.sprite = ImageUpgrade.sprite;
            shop.ChoisedUpgrade = upgrade;
        } else {
            shop.BuyUpgrade.SetActive(false);
            shop.ChoisedUpgrade = null;
        }
    }
}
