using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Order : MonoBehaviour
{
    public Image ImageFood;
    public Text Name;
    public Text Description;
    public Slider slider;
    public GameObject Final;
    public Text ingridientText;
    public Technic NeedTechnic;
    public TechnicTemplate template;
    private int Number;
    private ForOrders Orders;
    private ForIngridients Ingridients;
    public bool Starting;
    private Food NeedFood;
    public List<bool> BoolIngridients;
    public Button StartButton;

    public void SetOrder(ForOrders forOrders, Food food, int i, ForIngridients ingridients) {
        NeedFood = food;
        ImageFood.sprite = food.ImageFood;
        Name.text = food.Name;
        slider.maxValue = food.TimeToCooking;
        slider.value = 0;
        Number = i;
        Orders = forOrders;
        Ingridients = ingridients;
        NeedTechnic = food.TypeTechnic;
        UpdateIngridients();
    }

    void Update() {
        if (Starting) {
            if (slider.value < slider.maxValue) {
                slider.value += Time.deltaTime * template.FasterSpeed;
            } else {
                Orders.MadeOreders[Number] = true;
                Final.SetActive(true);
                Starting = false;
            }
        } else {
            if (BoolIngridients.IndexOf(false) == -1) {
                StartButton.interactable = true;
            } else {
                StartButton.interactable = false;
            }
        } 
    }

    public void UpdateIngridients() {
        if (!Starting && Final.activeSelf != true) {
            BoolIngridients.Clear();
            foreach (Transform child in Description.gameObject.transform)
                Destroy(child.gameObject);
            for (int j = 0; j < NeedFood.NeedIngridients.Count; j++) {
                var TextIngidient = Instantiate(ingridientText, Description.gameObject.transform);
                TextIngidient.gameObject.SetActive(true);
                int NumIng = Ingridients.HaveIngridients.IndexOf(NeedFood.NeedIngridients[j]);
                if (NumIng > -1) {
                    if (Ingridients.NumberIngridients[NumIng] >= NeedFood.NumberIngridients[j]) {
                        TextIngidient.color = new Color(76 / 255.0f, 175 / 255.0f, 79 / 255.0f);
                        BoolIngridients.Add(true);
                    } else {
                        TextIngidient.color = new Color(211 / 255.0f, 47 / 255.0f, 47 / 255.0f);
                        BoolIngridients.Add(false);
                    }
                } else {
                    TextIngidient.color = new Color(211 / 255.0f, 47 / 255.0f, 47 / 255.0f);
                    BoolIngridients.Add(false);
                }
                TextIngidient.text = "*" + NeedFood.NeedIngridients[j].Name + " - " + NeedFood.NumberIngridients[j];
            }
            var TextTechic = Instantiate(ingridientText, Description.gameObject.transform);
            TextTechic.gameObject.SetActive(true);
            TextTechic.text = "*" + NeedFood.TypeTechnic.Name;
            if (Orders.AvailableTechnic.Contains(NeedTechnic)) {
                if (Orders.AvailableTechnicTemplate[Orders.AvailableTechnic.IndexOf(NeedTechnic)].strength > 0 && !Orders.AvailableTechnicTemplate[Orders.AvailableTechnic.IndexOf(NeedTechnic)].Repairing && !Orders.AvailableTechnicTemplate[Orders.AvailableTechnic.IndexOf(NeedTechnic)].Starting){
                    TextTechic.color = new Color(76 / 255.0f, 175 / 255.0f, 79 / 255.0f);
                    BoolIngridients.Add(true);
                } else {
                    TextTechic.color = new Color(211 / 255.0f, 47 / 255.0f, 47 / 255.0f);
                    BoolIngridients.Add(false);
                }
            } else {
                TextTechic.color = new Color(211 / 255.0f, 47 / 255.0f, 47 / 255.0f);
                BoolIngridients.Add(false);
            }
        }
    }

    public void StartingCooking()
    {
        if (NewOrContinue.Education)
            Orders.education.AddIndex(false);
        foreach (Transform child in Description.gameObject.transform)
            Destroy(child.gameObject);
        Description.text = NeedFood.Description;
        StartButton.gameObject.SetActive(false);
        Starting = true;
        foreach (TechnicTemplate technic in Orders.AvailableTechnicTemplate) {
            if (technic.technic == NeedTechnic) {
                technic.Starting = true;
                technic.Work.Play();
                technic.GetComponent<Image>().sprite = technic.technic.ActiveIcon;
                technic.DescriptionFood.ImageFood.sprite = ImageFood.sprite;
                technic.DescriptionFood.Name.text = Name.text;
                technic.DescriptionFood.Description.text = Description.text;
                technic.DescriptionFood.slider.maxValue = slider.maxValue;
                technic.DescriptionFood.slider.value = 0;
                template = technic;
                break;
            }
        }
        for (int i = 0; i < NeedFood.NeedIngridients.Count; i++) {
            int NumIng = Ingridients.HaveIngridients.IndexOf(NeedFood.NeedIngridients[i]);
            if (Ingridients.NumberIngridients[NumIng] > NeedFood.NumberIngridients[i]) {
                Ingridients.NumberIngridients[NumIng] -= NeedFood.NumberIngridients[i];
            } else {
                Ingridients.NumberIngridients.RemoveAt(NumIng);
                Ingridients.HaveIngridients.RemoveAt(NumIng);
            }
            Ingridients.RenderIngridients();
        }
        Orders.UpgradeIngridientsOrders();
    }
}
