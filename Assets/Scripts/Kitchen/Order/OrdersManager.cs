using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(FoodManager))]
public class OrdersManager : MonoBehaviour
{
    [SerializeField] private KitchenStorage _kitchenStorage;
    [SerializeField] private TechnicManager _technicManager;
    private readonly List<Order> _orders = new();
    private readonly ClientState[] _suitableStates = { ClientState.Spawn, ClientState.Wait, ClientState.Sit };

    public event Action<Order> OrderAdded;
    public event Action<Order> OrderRemoved;

    public TechnicManager TechnicManager => _technicManager;
    public KitchenStorage KitchenStorage => _kitchenStorage;

    public void SetNewOrder(Client client)
    {
        if (!_suitableStates.Contains(client.ClientData.State))
            return;

        Order order = client.ClientData.Order;
        client.OrderActivated += AddOrder;
        client.ClientLeave += RemoveOrder;
        client.ClientEat += RemoveOrder;
        order.OrderFinished += client.CheckOrder;

        if (order.IsActivated)
            client.ActivateOrder();
    }

    private void AddOrder(Client client)
    {
        if (client.ClientData.Type == ClientType.GrayMan) {
            _kitchenStorage.RemoveAll();
            SaveManager.instance.Save();
            Application.Quit();
        }

        _orders.Add(client.ClientData.Order);
        OrderAdded?.Invoke(client.ClientData.Order);
    }

    private void RemoveOrder(Client client)
    {
        Order order = client.ClientData.Order;

        client.OrderActivated -= AddOrder;
        client.ClientLeave -= RemoveOrder;
        client.ClientEat -= RemoveOrder;

        if (GetOrderIndex(order) == -1)
            return;

        OrderRemoved?.Invoke(order);
        _technicManager.DisableTechnic(order.Food.TypeTechnic);
        _orders.Remove(order);
    }

    public int GetOrderIndex(Order order)
    {
        return _orders.IndexOf(order);
    }

    public void StartCook(Order order)
    {
        _kitchenStorage.RemoveIngredients(order.Food.Ingredients);
        _technicManager.ActivateTechnic(order);
    }
}
