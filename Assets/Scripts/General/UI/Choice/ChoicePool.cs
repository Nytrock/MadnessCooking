using UnityEngine;

public class ChoicePool<TItem, TButton> : Pool<TButton>
    where TItem : BuyableItem where TButton : ChoiceButton<TItem> {

    [SerializeField] private TButton _prefab;

    public override TButton GetObject() {
        if (_pool.Count == 0) {
            TButton newButton = Instantiate(_prefab, _container);
            newButton.ChangeState(true);
            return newButton;
        }

        TButton button = _pool.Dequeue();
        button.ChangeState(true);
        return button;
    }

    public override void PutObject(TButton button) {
        _pool.Enqueue(button);
        button.Disable();
    }
}
