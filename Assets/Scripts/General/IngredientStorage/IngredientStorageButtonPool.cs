using System.Collections.Generic;
using UnityEngine;

public class IngredientStorageButtonPool : MonoBehaviour
{
    [SerializeField] private IngredientStorageButton _prefab;
    [SerializeField] private Transform _container;

    private readonly Queue<IngredientStorageButton> _pool = new();

    public IngredientStorageButton GetObject(IngredientCount count)
    {
        if (_pool.Count == 0) {
            var button = Instantiate(_prefab, _container);
            button.gameObject.SetActive(false);
            button.SetVisual(count);
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
