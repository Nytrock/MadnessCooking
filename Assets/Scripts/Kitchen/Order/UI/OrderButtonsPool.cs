using System.Collections.Generic;
using UnityEngine;

public class OrderButtonsPool : MonoBehaviour
{
    [SerializeField] private OrderButton _prefab;
    [SerializeField] private Transform _container;
    [SerializeField] private TechnicManager _technicManager;
    [SerializeField] private KitchenStorage _kitchenStorage;
    private Queue<OrderButton> _pool;

    private void Awake()
    {
        _pool = new Queue<OrderButton>();
    }

    public OrderButton GetObject()
    {
        if (_pool.Count == 0) {
            OrderButton button = Instantiate(_prefab, _container);
            button.Setup(_technicManager, _kitchenStorage);
            _pool.Enqueue(button);
        }

        return _pool.Dequeue();
    }

    public void PutObject(OrderButton button)
    {
        _pool.Enqueue(button);
        button.Disable();
    }
}
