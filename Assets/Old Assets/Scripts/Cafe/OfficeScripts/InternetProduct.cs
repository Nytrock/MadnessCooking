using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InternetProduct : MonoBehaviour
{
    public Image ImageProduct;
    public LocalizedText NameProduct;
    public LocalizedText DescriptionProduct;
    public LocalizedText CostProduct;
    public Ingridient ingridient;
    public Food food;
    public Upgrade upgrade;
    public TechnicTemplate technicTempl;
    public InternetIngridientsFill IngridientsFill;
    public InternetFoodFill FoodFill;
    public InternetTechnicFill TechnicFill;
    public InternetUpgradeShop UpgradeFill;
    public Button BuyButton;

    private int costItem = -1;
    private int playerCoins;

    private void Start()
    {
        if (IngridientsFill != null || TechnicFill != null || TechnicFill != null)
            costItem = int.Parse(CostProduct.GetComponent<TextMeshProUGUI>().text);
    }

    void Update() {
        if (IngridientsFill != null)
            playerCoins = IngridientsFill.Money.Coins;
        else if (TechnicFill != null)
            playerCoins = TechnicFill.Money.Coins;
        else if (TechnicFill != null)
            playerCoins = UpgradeFill.Money.Coins;

        BuyButton.interactable = playerCoins >= costItem;
    }


    public void buy()
    {
        if (IngridientsFill != null)
            IngridientsFill.BuyIngridient(ingridient);
        else if (FoodFill != null) {
            FoodFill.Press.Play();
            FoodFill.ActiveFoodBuy(food);
        } else if (TechnicFill != null)
            TechnicFill.BuyTechnic(technicTempl);
        else if (UpgradeFill != null) {
            UpgradeFill.BuyUpgrade(upgrade);
        }
    }
}
