using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetProduct : MonoBehaviour
{
    public Image ImageProduct;
    public Text NameProduct;
    public Text DescriptionProduct;
    public Text CostProduct;
    public Ingridient ingridient;
    public Food food;
    public Upgrade upgrade;
    public TechnicTemplate technicTempl;
    public InternetIngridientsFill IngridientsFill;
    public InternetFoodFill FoodFill;
    public InternetTechnicFill TechnicFill;
    public InternetUpgradeShop UpgradeFill;
    public Button BuyButton;

    void Update() {
        if (IngridientsFill != null) {
            if (IngridientsFill.Money.Coins >= int.Parse(CostProduct.text))
                BuyButton.interactable = true;
            else
                BuyButton.interactable = false;
        } else if (TechnicFill != null) {
            if (TechnicFill.Money.Coins >= int.Parse(CostProduct.text))
                BuyButton.interactable = true;
            else
                BuyButton.interactable = false;
        } else if (UpgradeFill != null) {
            if (UpgradeFill.Money.Coins >= int.Parse(CostProduct.text))
                BuyButton.interactable = true;
            else
                BuyButton.interactable = false;
        }
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
