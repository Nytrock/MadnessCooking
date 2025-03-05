using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderRendererManager : MonoBehaviour {
    [SerializeField] private OrderRenderer[] _renderers;
    [SerializeField] private OrdersManager _manager;

    private int _countOfOrders;
    private List<int> _availableRenderers;
    private List<int> _takenRenderers;

    private void Awake() {
        _manager.OrderAdded += AddOrder;
        _manager.OrderRemoved += RemoveOrder;

        _availableRenderers = Enumerable.Range(0, _renderers.Length).ToList();
        _takenRenderers = new();
        _countOfOrders = 0;
    }

    private void AddOrder(Order order) {
        _countOfOrders++;
        if (_availableRenderers.Count == 0)
            return;

        int rendererIndex = _availableRenderers.PopRandom();
        _takenRenderers.Insert(0, rendererIndex);
        _renderers[rendererIndex].ChangeState(true);
    }

    private void RemoveOrder(Order order) {
        _countOfOrders--;
        if (_countOfOrders > _renderers.Length)
            return;

        int rendererIndex = _takenRenderers.Pop(0);
        _availableRenderers.Add(rendererIndex);
        _renderers[rendererIndex].ChangeState(false);
    }
}