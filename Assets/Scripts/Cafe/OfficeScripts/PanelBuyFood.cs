using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelBuyFood : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public Image IngrImage;
    public Button BuyButton;
    public TextMeshProUGUI TextPrefab;
    public Transform Container;
    public TextMeshProUGUI CostText;
    public int Cost;
    public MoneyBar Money;
    private Food Fooding;
    public InternetFoodFill internetFood;

    void Update()
    {
        if (GetComponent<Animator>().GetBool("Active")) {
            if (Money.Coins >= Cost)
                BuyButton.interactable = true;
            else
                BuyButton.interactable = false;
        }
    }

    public void FiilPanel(Food food)
    {
        Name.text = LocalizationManager.GetTranslate(food.Name);
        Description.text = LocalizationManager.GetTranslate(food.Description);
        IngrImage.sprite = food.ImageFood;
        foreach (Transform child in Container)
            Destroy(child.gameObject);
        for (int i = 0; i < food.NeedIngridients.Count; i++) {
            var descr = Instantiate(TextPrefab, Container);
            descr.text = "* " + LocalizationManager.GetTranslate(food.NeedIngridients[i].Name) + " - " + food.NumberIngridients[i];
        }
        var tesh = Instantiate(TextPrefab, Container);
        tesh.text = "* " + LocalizationManager.GetTranslate(food.TypeTechnic.Name);
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
