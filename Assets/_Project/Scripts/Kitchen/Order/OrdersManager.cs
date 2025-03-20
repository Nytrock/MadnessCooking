using System;
using UnityEngine;

public class OrdersManager : MonoBehaviour {
    [SerializeField] private KitchenStorage _kitchenStorage;
    [SerializeField] private TechnicManager _technicManager;
    [SerializeField] private GameSaveManager _saveManager;
    [SerializeField] private TutorialManager _tutorialManager;

    public event Action<Order> OrderAdded;
    public event Action<Order> OrderRemoved;

    public void AddNewClient(Client client) {
        if (client.Data.State == ClientState.Leave || client.Data.IsServiced)
            return;

        Order order = client.Data.Order;
        client.OrderActivated += AddOrder;
        client.ClientRejected += RemoveOrder;
        client.ClientEat += RemoveOrder;
        order.OrderFinished += client.FinishOrder;

        if (order.IsActivated)
            client.ActivateOrder();

        if (order.IsFinished)
            client.FinishOrder();
    }

    private void AddOrder(Client client) {
        if (client.Data.Type == ClientType.GrayMan) {
            _saveManager.Save();
            Application.Quit();
        }

        client.OrderActivated -= AddOrder;

        if (_tutorialManager.IsWork)
            _tutorialManager.NextTutorialPart();
        OrderAdded?.Invoke(client.Data.Order);
    }

    private void RemoveOrder(Client client) {
        Order order = client.Data.Order;
        if (!order.IsActivated)
            return;

        if (_tutorialManager.IsWork)
            _tutorialManager.NextTutorialPart();

        client.ClientRejected -= RemoveOrder;
        client.ClientEat -= RemoveOrder;
        order.OrderFinished -= client.FinishOrder;

        if (order.IsCooking)
            _technicManager.EmergencyStopCooking(order);
        OrderRemoved?.Invoke(order);
    }

    public void StartCook(Order order) {
        _kitchenStorage.RemoveIngredients(order.Food.Ingredients);
        _technicManager.StartCooking(order);
    }
}
