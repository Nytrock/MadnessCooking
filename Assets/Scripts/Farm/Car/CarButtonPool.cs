using System.Collections.Generic;
using UnityEngine;

public class CarButtonPool : MonoBehaviour
{
    [SerializeField] private CarButton _prefab;
    [SerializeField] private Transform _container;

    private Queue<CarButton> _pool;

    private void Awake()
    {
        _pool = new Queue<CarButton>();
    }

    public CarButton GetObject()
    {
        if (_pool.Count == 0) {
            var button = Instantiate(_prefab, _container);
            button.gameObject.SetActive(false);
            _pool.Enqueue(button);
        }

        return _pool.Dequeue();
    }

    public void PutObject(CarButton button)
    {
        _pool.Enqueue(button);
        button.gameObject.SetActive(false);
    }
}
