using System.Collections.Generic;
using UnityEngine;

public class ChoicePool<T, K> : MonoBehaviour where K: ChoiceButton<T>
{
    [SerializeField] private K _prefab;
    [SerializeField] private Transform _container;

    private Queue<K> _pool = new();


    public K GetObject()
    {
        if (_pool.Count == 0) {
            var newButton = Instantiate(_prefab, _container);
            newButton.ChangeState(true);
            return newButton;
        }

        var button = _pool.Dequeue();
        button.ChangeState(true);
        return button;
    }

    public void PutObject(K button)
    {
        _pool.Enqueue(button);
        button.Disable();
    }
}
