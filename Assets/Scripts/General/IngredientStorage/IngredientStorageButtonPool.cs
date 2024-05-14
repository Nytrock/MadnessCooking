using System.Collections.Generic;
using UnityEngine;

public class IngredientStorageButtonPool : MonoBehaviour
{
    [SerializeField] private IngredientStorageButton _prefab;
    [SerializeField] private Transform _container;

    private readonly Queue<IngredientStorageButton> _pool = new();

    public IngredientStorageButton GetObject(IngredientCount count)
    {
        IngredientStorageButton button;
        if (_pool.Count == 0) {
            button = Instantiate(_prefab, _container);
        } else {
            button = _pool.Dequeue();
        }

        button.SetVisual(count);
        button.gameObject.SetActive(true);
        return button;
    }

    public void PutObject(IngredientStorageButton button)
    {
        _pool.Enqueue(button);
        button.gameObject.SetActive(false);
    }
}
