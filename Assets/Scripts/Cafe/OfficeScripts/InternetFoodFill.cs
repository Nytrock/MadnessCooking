using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetFoodFill: MonoBehaviour
{
    public Transform ContainerContainers;
    public Transform Container;
    public InternetProduct product;
    public List<Food> AllFood;
    public List<GameObject> Containers;
    public Cafe cafe;
    public MoneyBar Money;
    public int indexContainer;
    public Button Left;
    public Button Right;
    public PanelBuyFood buyFood;
    public SpawnClients spawnClients;
    public AudioSource Press;

    void Start()
    {
        UpdateFood();
    }

    void Update()
    {
        for (int i = 0; i < Containers.Count; i++) {
            if (i == indexContainer)
                Containers[i].SetActive(true);
            else
                Containers[i].SetActive(false);
        }
    }

    public void UpdateFood() {
        int CountContainers = 0;
        if (AllFood.Count % 8 == 0)
            CountContainers = AllFood.Count / 8;
        else
            CountContainers = AllFood.Count / 8 + 1;
        foreach (Transform cont in ContainerContainers)
            Destroy(cont.gameObject);
        Containers.Clear();
        for (int i = 0; i < CountContainers; i++) {
            var container = Instantiate(Container, ContainerContainers);
            Containers.Add(container.gameObject);
            for (int j = 8 * i; j < 8 + 8 * i; j++) {
                if (j < AllFood.Count) {
                    var IngridientCell = Instantiate(product, container);
                    IngridientCell.NameProduct.text = AllFood[j].Name;
                    IngridientCell.ImageProduct.sprite = AllFood[j].ImageFood;
                    IngridientCell.DescriptionProduct.text = AllFood[j].Description;
                    IngridientCell.CostProduct.text = "Buy";
                    IngridientCell.FoodFill = this;
                    IngridientCell.food = AllFood[j];
                } else {
                    break;
                }
            }
        }
        if (indexContainer >= Containers.Count)
            indexContainer -= 1;
        UpdateButton();
    }

    public void ActiveFoodBuy(Food food)
    {
        buyFood.FiilPanel(food);
        buyFood.GetComponent<Animator>().SetBool("Active", true);
    }

    public void NoActiveFoodBuy()
    {
        buyFood.GetComponent<Animator>().SetBool("Active", false);
        Press.Play();
    }

    public void BuyFood(Food food)
    {
        Press.Play();
        Money.AddCoins(-food.CostBuy);
        AllFood.Remove(food);
        cafe.AvailableFood.Add(food);
        spawnClients.FoodMultiply += 0.1f;
        UpdateFood();
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
