using System.Collections.Generic;
using UnityEngine;

public class IngredientStorageButtonPool : MonoBehaviour
{
    [SerializeField] private IngredientStorageButton _prefab;
    [SerializeField] private Transform _container;

    private Queue<IngredientStorageButton> _pool;

    private void Awake()
    {
        _pool = new Queue<IngredientStorageButton>();
    }

    public IngredientStorageButton GetObject()
    {
        if (_pool.Count == 0) {
            var button = Instantiate(_prefab, _container);
            button.gameObject.SetActive(false);
            _pool.Enqueue(button);
        }

        return _pool.Dequeue();
    }

    public void PutObject(IngredientStorageButton button)
    {
        _pool.Enqueue(button);
        button.gameObject.SetActive(false);
    }
}
