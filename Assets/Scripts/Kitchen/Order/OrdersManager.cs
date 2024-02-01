using System;
using System.Collections.Generic;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    [SerializeField] private CafeOpener _cafeOpener;
    [SerializeField] private KitchenBox _box;
    [SerializeField] private TechnicManager _technicManager;
    [SerializeField] private List<Order> _orders = new();
    private FoodManager _foodManager;

    public event Action<Order> OrderAdded;
    public event Action<Order> OrderRemoved;
    public event Action OrderFinished;

    public TechnicManager TechnicManager => _technicManager;
    public KitchenBox Box => _box;

    private void Start()
    {
        _cafeOpener.CafeChanged += RemoveOrders;
        _foodManager = GetComponent<FoodManager>();
    }

    private void RemoveOrders()
    {
        _orders.Clear();
    }

    public void SetNewOrder(Client client, CafeSpot spot)
    {
        var food = _foodManager.GetRandomFood();
        var order = new Order {
            TableNumber = spot.Index,
            Food = food,
        };

        client.SetOrder(order);
        client.OrderActivated += AddOrder;
        client.ClientLeave += RemoveOrder;
        OrderFinished += client.CheckOrder;
    }

    private void AddOrder(Client client)
    {
        _orders.Add(client.Order);
        OrderAdded?.Invoke(client.Order);
    }


    public void FinishOrder()
    {
        OrderFinished?.Invoke();
    }

    private void RemoveOrder(Client client)
    {
        OrderFinished -= client.CheckOrder;
        client.OrderActivated -= AddOrder;
        client.ClientLeave -= RemoveOrder;

        if (GetOrderId(client.Order) == -1)
            return;

        OrderRemoved?.Invoke(client.Order);
        _orders.Remove(client.Order);
    }

    public int GetOrderId(Order order)
    {
        return _orders.IndexOf(order);
    }
}
