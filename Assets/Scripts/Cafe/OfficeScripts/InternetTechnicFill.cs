using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternetTechnicFill : MonoBehaviour
{
    public Transform ContainerContainers;
    public Transform Container;
    public InternetProduct product;
    public List<TechnicTemplate> AllTechicTemplate;
    public List<GameObject> Containers;
    public ForOrders orders;
    public MoneyBar Money;
    public int indexContainer;
    public Button Left;
    public Button Right;
    public AudioSource Press;
    public Education education;

    void Start()
    {
        UpdateTechnic();
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


    public void UpdateTechnic()
    {
        int CountContainers = 0;
        if (AllTechicTemplate.Count % 8 == 0)
            CountContainers = AllTechicTemplate.Count / 8;
        else
            CountContainers = AllTechicTemplate.Count / 8 + 1;
        foreach (Transform cont in ContainerContainers)
            Destroy(cont.gameObject);
        Containers.Clear();
        for (int i = 0; i < CountContainers; i++) {
            var container = Instantiate(Container, ContainerContainers);
            Containers.Add(container.gameObject);
            for (int j = 8 * i; j < 8 + 8 * i; j++) {
                if (j < AllTechicTemplate.Count) {
                    var IngridientCell = Instantiate(product, container);
                    IngridientCell.NameProduct.text = AllTechicTemplate[j].technic.Name;
                    IngridientCell.ImageProduct.sprite = AllTechicTemplate[j].technic.MiniIcon;
                    IngridientCell.DescriptionProduct.text = AllTechicTemplate[j].technic.Description;
                    IngridientCell.CostProduct.text = AllTechicTemplate[j].technic.Cost.ToString();
                    IngridientCell.TechnicFill = this;
                    IngridientCell.technicTempl = AllTechicTemplate[j];
                } else {
                    break;
                }
            }
        }
        if (indexContainer >= Containers.Count)
            indexContainer -= 1;
        UpdateButton();
    }

    public void BuyTechnic(TechnicTemplate template)
    {
        Press.Play();
        education.AddIndex(false);
        Money.AddCoins(-template.technic.Cost);
        orders.AvailableTechnic.Add(template.technic);
        orders.AvailableTechnicTemplate.Add(template);
        template.gameObject.SetActive(true);
        AllTechicTemplate.Remove(template);
        UpdateTechnic();
        orders.UpgradeIngridientsOrders();
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
