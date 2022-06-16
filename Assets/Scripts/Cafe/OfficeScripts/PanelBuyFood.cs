using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBuyFood : MonoBehaviour
{
    public Text Name;
    public Text Description;
    public Image IngrImage;
    public Button BuyButton;
    public Text TextPrefab;
    public Transform Container;
    public Text CostText;
    public int Cost;
    public MoneyBar Money;
    private Food Fooding;
    public InternetFoodFill internetFood;

    void Update()
    {
        if (this.GetComponent<Animator>().GetBool("Active")) {
            if (Money.Coins >= Cost)
                BuyButton.interactable = true;
            else
                BuyButton.interactable = false;
        }
    }

    public void FiilPanel(Food food)
    {
        Name.text = food.Name;
        Description.text = food.Description;
        IngrImage.sprite = food.ImageFood;
        foreach (Transform child in Container)
            Destroy(child.gameObject);
        for (int i = 0; i < food.NeedIngridients.Count; i++) {
            var descr = Instantiate(TextPrefab, Container);
            descr.text = "*" + food.NeedIngridients[i].Name + " - " + food.NumberIngridients[i];
        }
        var tesh = Instantiate(TextPrefab, Container);
        tesh.text = "*" + food.TypeTechnic.Name;
        Cost = food.CostBuy;
        CostText.text = Cost.ToString();
        Fooding = food;
    }

    public void BuyFood()
    {
        this.GetComponent<Animator>().SetBool("Active", false);
        internetFood.BuyFood(Fooding);
    }
}
