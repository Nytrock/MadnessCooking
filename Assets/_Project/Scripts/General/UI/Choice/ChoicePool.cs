using UnityEngine;

public class ChoicePool<TItem, TButton> : Pool<TButton>
    where TItem : BuyableItem where TButton : ChoiceButton<TItem> {

    [SerializeField] private TButton _prefab;

    public override TButton GetObject() {
        TButton button = base.GetObject();
        button.transform.SetAsLastSibling();
        button.ChangeState(true);
        return button;
    }

    protected override TButton CreateObject() {
        return Instantiate(_prefab, _container);
    }

    public override void PutObject(TButton button) {
        base.PutObject(button);
        button.Disable();
    }
}
