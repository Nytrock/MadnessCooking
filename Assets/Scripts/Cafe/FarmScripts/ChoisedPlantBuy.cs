using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoisedPlantBuy : MonoBehaviour
{
    public Image Images;
    public Text Name;
    public Text Description;
    public Text CostText;
    public int Cost;
    public Button BuyButton;
    public ChoiceUpgradeButton BuyingUpgrade;
    public GameObject Container;
    public MoneyBar money;

    void Update()
    {
        if (BuyingUpgrade == null)
            Container.SetActive(false);
        else
            Container.SetActive(true);
        if (money.Coins < Cost)
            BuyButton.interactable = false;
        else
            BuyButton.interactable = true;
    }

    public void BuyUpgrade()
    {
        money.AddCoins(-Cost);
        BuyingUpgrade.BuyUpgrade();
    }
}
