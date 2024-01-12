using System;
using System.Collections.Generic;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    [SerializeField] private CafeOpener _cafeOpener;
    [SerializeField] private List<Order> _orders = new List<Order>();
    private FoodManager _foodManager;

    public event Action OrderFinished;

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
        OrderFinished += client.CheckOrder;
    }

    private void AddOrder(Order newOrder)
    {
        _orders.Add(newOrder);
    }

    [ContextMenu("FinishOrder")]
    public void FinishOrder()
    {
        OrderFinished?.Invoke();
    }
}
