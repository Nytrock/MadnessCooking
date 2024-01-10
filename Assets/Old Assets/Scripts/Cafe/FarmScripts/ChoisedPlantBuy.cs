using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoisedPlantBuy : MonoBehaviour
{
    public Image Images;
    public LocalizedText Name;
    public LocalizedText Description;
    public TextMeshProUGUI CostText;
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
