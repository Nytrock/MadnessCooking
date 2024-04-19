using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(FoodManager))]
public class OrdersManager : MonoBehaviour
{
    [SerializeField] private KitchenStorage _kitchenStorage;
    [SerializeField] private TechnicManager _technicManager;
    private List<Order> _orders = new();
    private ClientState[] _suitableStates = { ClientState.Spawn, ClientState.Wait, ClientState.Sit };

    public event Action<Order> OrderAdded;
    public event Action<Order> OrderRemoved;

    public TechnicManager TechnicManager => _technicManager;
    public KitchenStorage KitchenStorage => _kitchenStorage;

    public void SetNewOrder(Client client, CafeSpot spot)
    {
        if (!_suitableStates.Contains(client.ClientData.State))
            return;

        var order = new Order(client.ClientData.OrderFood, spot.Index + 1);
        client.SetOrder(order);
        client.OrderActivated += AddOrder;
        client.ClientLeave += RemoveOrder;
        client.ClientEat += RemoveOrder;
        order.OrderFinished += client.CheckOrder;

        if (client.ClientData.OrderActivated)
            client.ActivateOrder();
    }

    private void AddOrder(Client client)
    {
        if (client.ClientData.Type == ClientType.GrayMan) {
            _kitchenStorage.RemoveAll();
            SaveManager.instance.SaveAll();
            Application.Quit();
        }

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
        _kitchenStorage.RemoveIngredients(order.Food.Ingredients);
        _technicManager.ActivateTechnic(order);
    }
}
