using System.Collections.Generic;
using UnityEngine;

public class OrdersPool : MonoBehaviour
{
    [SerializeField] private OrderButton _prefab;
    [SerializeField] private Transform _container;

    private OrdersManager _manager;
    private OrdersUI _ordersUI;
    private Queue<OrderButton> _pool;

    private void Awake()
    {
        _pool = new Queue<OrderButton>();
    }

    public OrderButton GetObject()
    {
        if (_pool.Count == 0) {
            var button = Instantiate(_prefab, _container);
            button.SetStorages(_manager, _ordersUI);
            _pool.Enqueue(button);
        }

        return _pool.Dequeue();
    }

    public void PutObject(OrderButton button)
    {
        _pool.Enqueue(button);
        button.Disable();
    }

    public void SetStorages(OrdersManager manager, OrdersUI ordersUI)
    {
        _manager = manager;
        _ordersUI = ordersUI;
    }
}
