using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForOrders : MonoBehaviour
{
    public List<Food> OrdersClients;
    public List<Order> Orders;
    public List<bool> MadeOreders;
    public Animator PanelOrders;
    public ButtonMenu KitchenButton;
    public Order PatternOrder;
    public List<GameObject> VisualOrderers;
    public Transform Panel;
    public List<Technic> AvailableTechnic;
    public List<TechnicTemplate> AvailableTechnicTemplate;
    public AudioSource Open;
    public Education education;

    void Update()
    {
        if (KitchenButton.Looking.transform.position.x != KitchenButton.PositionLocation.position.x || KitchenButton.Looking.transform.position.y != KitchenButton.PositionLocation.position.y)
            PanelOrders.GetComponent<Animator>().SetBool("Active", false);
    }

    public void ActivePanel()
    {
        PanelOrders.SetBool("Active", !PanelOrders.GetBool("Active"));
        Open.Play();
    }

    public void AddFood(Food food, Client client)
    {
        OrdersClients.Add(food);
        MadeOreders.Add(false);
        var order = Instantiate(PatternOrder, Panel);
        order.SetOrder(this, food, OrdersClients.Count - 1, client.cafe.ingridients);
        Orders.Add(order);
        foreach (GameObject game in VisualOrderers)
            game.SetActive(false);
        for (int i = 0; i < MadeOreders.Count; i++)
            VisualOrderers[Random.Range(0, VisualOrderers.Count)].SetActive(true);
    }

    public void UpgradeIngridientsOrders()
    {
        for(int i = 0; i < Orders.Count; i++) {
            Orders[i].UpdateIngridients();
        }
    }
}
