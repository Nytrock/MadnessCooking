using UnityEngine;

public class OrderButtonsPool : Pool<OrderButton> {
    [SerializeField] private OrderButton _prefab;

    public override OrderButton GetObject() {
        if (_pool.Count == 0) {
            OrderButton button = Instantiate(_prefab, _container);
            _pool.Enqueue(button);
        }

        return _pool.Dequeue();
    }

    public override void PutObject(OrderButton button) {
        _pool.Enqueue(button);
        button.Disable();
    }
}
