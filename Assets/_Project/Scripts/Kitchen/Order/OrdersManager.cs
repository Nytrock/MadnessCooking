using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrdersManager : MonoBehaviour {
    [SerializeField] private KitchenStorage _kitchenStorage;
    [SerializeField] private TechnicManager _technicManager;
    [SerializeField] private GameSaveManager _saveManager;
    [SerializeField] private TutorialManager _tutorialManager;
    private readonly List<Order> _orders = new();
    private readonly ClientState[] _suitableStates = { ClientState.Spawn, ClientState.Sit };

    public event Action<Order> OrderAdded;
    public event Action<Order> OrderRemoved;

    public void SetNewOrder(Client client) {
        if (!_suitableStates.Contains(client.Data.State))
            return;

        Order order = client.Data.Order;
        client.OrderActivated += AddOrder;
        client.ClientRejected += RemoveOrder;
        client.ClientEat += RemoveOrder;
        order.OrderFinished += client.CheckOrder;

        if (order.IsActivated)
            client.ActivateOrder();

        if (order.IsFinished)
            client.CheckOrder();
    }

    private void AddOrder(Client client) {
        if (client.Data.Type == ClientType.GrayMan) {
            _kitchenStorage.RemoveAll();
            _saveManager.Save();
            Application.Quit();
        }

        client.OrderActivated -= AddOrder;

        if (_tutorialManager.IsWork)
            _tutorialManager.NextTutorialPart();
        _orders.Add(client.Data.Order);
        OrderAdded?.Invoke(client.Data.Order);
    }

    private void RemoveOrder(Client client) {
        Order order = client.Data.Order;
        if (_tutorialManager.IsWork)
            _tutorialManager.NextTutorialPart();

        client.ClientRejected -= RemoveOrder;
        client.ClientEat -= RemoveOrder;
        order.OrderFinished -= client.CheckOrder;

        if (GetOrderIndex(order) == -1)
            return;

        if (order.IsCooking)
            _technicManager.EmergencyStopCooking(order);
        OrderRemoved?.Invoke(order);
        _orders.Remove(order);
    }

    public int GetOrderIndex(Order order) {
        return _orders.IndexOf(order);
    }

    public void StartCook(Order order) {
        _kitchenStorage.RemoveIngredients(order.Food.Ingredients);
        _technicManager.StartCooking(order);
    }
}
