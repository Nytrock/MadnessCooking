using System;
using System.Collections.Generic;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    [SerializeField] private KitchenStorage _kitchenStorage;
    [SerializeField] private TechnicManager _technicManager;
    private List<Order> _orders = new();
    private FoodManager _foodManager;

    public event Action<Order> OrderAdded;
    public event Action<Order> OrderRemoved;

    public TechnicManager TechnicManager => _technicManager;
    public KitchenStorage KitchenStorage => _kitchenStorage;

    private void Start()
    {
        _foodManager = GetComponent<FoodManager>();
    }

    public void SetNewOrder(Client client, CafeSpot spot)
    {
        var food = _foodManager.GetRandomFood();
        var order = new Order(food, spot.Index + 1);

        client.SetOrder(order);
        client.OrderActivated += AddOrder;
        client.ClientLeave += RemoveOrder;
        client.ClientEat += RemoveOrder;
        order.OrderFinished += client.CheckOrder;
    }

    private void AddOrder(Client client)
    {
        _orders.Add(client.Order);
        OrderAdded?.Invoke(client.Order);
    }

    private void RemoveOrder(Client client)
    {
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
        _kitchenStorage.RemoveIngrediens(order.Food.Ingredients);
        _technicManager.ActivateTechnic(order);
    }
}
