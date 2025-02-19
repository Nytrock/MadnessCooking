using System;
using System.Linq;
using UnityEngine;

public class OrdersManager : MonoBehaviour {
    [SerializeField] private KitchenStorage _kitchenStorage;
    [SerializeField] private TechnicManager _technicManager;
    [SerializeField] private GameSaveManager _saveManager;
    [SerializeField] private TutorialManager _tutorialManager;
    private readonly ClientState[] _suitableStates = { ClientState.Spawn, ClientState.WaitOthers, ClientState.WaitOrder };

    public event Action<Order> OrderAdded;
    public event Action<Order> OrderRemoved;

    public void SetNewOrder(Client client) {
        if (!_suitableStates.Contains(client.Data.State) || client.Data.IsServiced)
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
        if (_tutorialManager.IsWork)
            _tutorialManager.NextTutorialPart();

        client.ClientRejected -= RemoveOrder;
        client.ClientEat -= RemoveOrder;
        order.OrderFinished -= client.CheckOrder;

        if (order.IsCooking)
            _technicManager.EmergencyStopCooking(order);
        OrderRemoved?.Invoke(order);
    }

    public void StartCook(Order order) {
        _kitchenStorage.RemoveIngredients(order.Food.Ingredients);
        _technicManager.StartCooking(order);
    }
}
