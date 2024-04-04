using UnityEngine;

public abstract class ChoiceBuyUI<T> : ChoiceUI<T, ChoiceBuyButton<T>> where T: BuyableObject
{
    [SerializeField] protected ChoiceBuyDescriptionUI _description;

    public virtual void Choice(int index, bool isBuyable)
    {
        if (_chosedIndex == -1)
            _description.ChangeActive();
        else
            _choiceButtons[_chosedIndex].ChangeSelectedState();

        var isSame = index == _chosedIndex;
        if (isSame) {
            _chosedIndex = -1;
            _description.ChangeActive();
            return;
        }

        _chosedIndex = index;
        _choiceButtons[_chosedIndex].ChangeSelectedState();
        _submitButton.interactable = !isSame && isBuyable;
    }

    protected override void Activate()
    {
        base.Activate();
        _description.ChangeActive(false);
    }
}
