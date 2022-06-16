using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetIngridientsFill : MonoBehaviour
{
    public Transform ContainerContainers;
    public Transform Container;
    public InternetProduct product;
    public List<Ingridient> AllIngridient;
    public List<GameObject> Containers;
    public Farm farm;
    public MoneyBar Money;
    public int indexContainer;
    public Button Left;
    public Button Right;
    public ForIngridients forIngridients;
    public AudioSource Press;
    public Education education;
    public ForOrders forOrders;

    void Start()
    {
        UpdateIngridients();
    }

    void Update() {
        for (int i = 0; i < Containers.Count; i++) {
            if (i == indexContainer)
                Containers[i].SetActive(true);
            else
                Containers[i].SetActive(false);
        }
    }


    public void UpdateIngridients()
    {
        int CountContainers = 0;
        if (AllIngridient.Count % 8 == 0)
            CountContainers = AllIngridient.Count / 8;
        else
            CountContainers = AllIngridient.Count / 8 + 1;
        foreach (Transform cont in ContainerContainers)
            Destroy(cont.gameObject);
        Containers.Clear();
        for (int i = 0; i < CountContainers; i++) {
            var container = Instantiate(Container, ContainerContainers);
            Containers.Add(container.gameObject);
            for (int j = 8 * i; j < 8 + 8 * i; j++) {
                if (j < AllIngridient.Count) {
                    var IngridientCell = Instantiate(product, container);
                    IngridientCell.NameProduct.text = AllIngridient[j].Name;
                    IngridientCell.ImageProduct.sprite = AllIngridient[j].ImageIngridient;
                    IngridientCell.DescriptionProduct.text = AllIngridient[j].Description;
                    IngridientCell.CostProduct.text = AllIngridient[j].Cost.ToString();
                    IngridientCell.IngridientsFill = this;
                    IngridientCell.ingridient = AllIngridient[j];
                } else {
                    break;
                }
            }
        }
        if (indexContainer >= Containers.Count)
            indexContainer -= 1;
        UpdateButton();
    }

    public void BuyIngridient(Ingridient ingridient)
    {
        Press.Play();
        education.AddIndex(false);
        Money.AddCoins(-ingridient.Cost);
        if (ingridient.Name != "Spices") {
            AllIngridient.Remove(ingridient);
            farm.AvailibleIngridients.Add(ingridient);
            UpdateIngridients();
        } else {
            if (!forIngridients.HaveIngridients.Contains(ingridient)) {
                forIngridients.HaveIngridients.Add(ingridient);
                forIngridients.NumberIngridients.Add(1);
            } else {
                forIngridients.NumberIngridients[forIngridients.HaveIngridients.IndexOf(ingridient)] += 1;
            }
            forOrders.UpgradeIngridientsOrders();
            forIngridients.RenderIngridients();
        }
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
