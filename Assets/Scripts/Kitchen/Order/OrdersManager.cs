using System;
using System.Collections.Generic;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    [SerializeField] private KitchenBox _box;
    [SerializeField] private TechnicManager _technicManager;
    private List<Order> _orders = new();
    private FoodManager _foodManager;

    public event Action<Order> OrderAdded;
    public event Action<Order> OrderRemoved;
    public event Action OrderFinished;

    public TechnicManager TechnicManager => _technicManager;
    public KitchenBox Box => _box;

    private void Start()
    {
        _foodManager = GetComponent<FoodManager>();
    }

    public void SetNewOrder(Client client, CafeSpot spot)
    {
        var food = _foodManager.GetRandomFood();
        var order = new Order {
            TableNumber = spot.Index + 1,
            Food = food,
        };

        client.SetOrder(order);
        client.OrderActivated += AddOrder;
        client.ClientLeave += RemoveOrder;
        client.ClientEat += RemoveOrder;
        OrderFinished += client.CheckOrder;
    }

    private void AddOrder(Client client)
    {
        _orders.Add(client.Order);
        OrderAdded?.Invoke(client.Order);
    }

    private void RemoveOrder(Client client)
    {
        OrderFinished -= client.CheckOrder;
        client.OrderActivated -= AddOrder;
        client.ClientLeave -= RemoveOrder;
        client.ClientEat -= RemoveOrder;

        if (GetOrderId(client.Order) == -1)
            return;

        OrderRemoved?.Invoke(client.Order);
        _technicManager.DisableTechnic(client.Order.Food.TypeTechnic);
        _orders.Remove(client.Order);
    }

    public int GetOrderId(Order order)
    {
        return _orders.IndexOf(order);
    }

    public void StartCook(Order order)
    {
        _box.RemoveIngrediens(order.Food.Ingredients);
        _technicManager.ActivateTechnic(order.Food);
    }

    public void FinishCook()
    {
        OrderFinished?.Invoke();
    }
}
