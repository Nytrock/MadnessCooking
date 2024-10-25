using UnityEngine;

public abstract class ChoiceBuyUI<TItem> : ChoiceUI<TItem, ChoiceBuyButton<TItem>>
    where TItem : BuyableItem {

    [SerializeField] protected ChoiceBuyDescriptionUI _description;

    public virtual void Choice(int index, bool isBuyable) {
        if (_chosedIndex == -1)
            _description.ChangeState();
        else
            _choiceButtons[_chosedIndex].ChangeChoosedState();

        bool isSame = index == _chosedIndex;
        if (isSame) {
            _chosedIndex = -1;
            _description.ChangeState();
            return;
        }

        _chosedIndex = index;
        _choiceButtons[_chosedIndex].ChangeChoosedState();
        _submitButton.interactable = !isSame && isBuyable;
    }

    public override void SetChoice() {
        MoneyManager.Instance.ChangeMoney(-_choiceButtons[_chosedIndex].Item.Price);
    }

    protected override void Activate() {
        base.Activate();
        _description.ChangeState(false);
    }
}
