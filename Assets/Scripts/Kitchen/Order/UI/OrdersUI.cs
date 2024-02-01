using System.Collections.Generic;
using UnityEngine;

public class OrdersUI : MonoBehaviour
{
    [SerializeField] private OrdersManager _manager;
    [SerializeField] private OrdersPool _pool;
    [SerializeField] private GameObject _panel;
    private List<OrderButton> _orderButtons = new();

    private void Start()
    {
        _manager.OrderAdded += AddOrder;
        _manager.OrderRemoved += RemoveOrder;
        _pool.SetStorages(_manager.Box, _manager.TechnicManager);
        _panel.SetActive(false);
    }

    public void ChangeState()
    {
        _panel.SetActive(!_panel.activeSelf);
    }

    private void AddOrder(Order order)
    {
        OrderButton button = _pool.GetObject();
        button.StartSetup(order);
        _orderButtons.Add(button);
    }

    private void RemoveOrder(Order order)
    {
        int index = _manager.GetOrderId(order);
        _pool.PutObject(_orderButtons[index]);
        _orderButtons.RemoveAt(index);
    }

}
