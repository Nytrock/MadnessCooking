using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrdersRenderer : MonoBehaviour {
    [SerializeField] private OrderRenderer[] _orderRenderers;
    [SerializeField] private OrdersManager _manager;
    private List<OrderRenderer> _availableRenderers;

    private void Awake() {
        _manager.OrderAdded += AddOrder;
        _manager.OrderRemoved += RemoveOrder;
        _availableRenderers = _orderRenderers.ToList();
    }

    private void AddOrder(Order order) {
        if (_availableRenderers.Count == 0)
            return;

        int index = Random.Range(0, _availableRenderers.Count);
        _availableRenderers[index].Enable(order);
        _availableRenderers.RemoveAt(index);
    }

    private void RemoveOrder(Order order) {
        foreach (var renderer in _orderRenderers) {
            if (renderer.Order == order) {
                renderer.Disable();
                _availableRenderers.Add(renderer);
                break;
            }
        }
    }
}