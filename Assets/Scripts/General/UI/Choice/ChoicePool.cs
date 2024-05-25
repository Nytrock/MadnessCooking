using System.Collections.Generic;
using UnityEngine;

public class ChoicePool<TItem, TButton> : MonoBehaviour where TItem: BuyableObject where TButton: ChoiceButton<TItem>
{
    [SerializeField] private TButton _prefab;
    [SerializeField] private Transform _container;

    private Queue<TButton> _pool = new();

    public TButton GetObject()
    {
        if (_pool.Count == 0) {
            TButton newButton = Instantiate(_prefab, _container);
            newButton.ChangeState(true);
            return newButton;
        }

        TButton button = _pool.Dequeue();
        button.ChangeState(true);
        return button;
    }

    public void PutObject(TButton button)
    {
        _pool.Enqueue(button);
        button.Disable();
    }
}
