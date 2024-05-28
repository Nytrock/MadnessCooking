public abstract class ChoiceSimpleUI<TItem> : ChoiceUI<TItem, ChoiceSimpleButton<TItem>> where TItem : BuyableObject {
    public void Choice(int index) {
        if (_chosedIndex != -1)
            SetSelectedState(_chosedIndex);

        _submitButton.interactable = index != _chosedIndex;
        if (index == _chosedIndex) {
            _chosedIndex = -1;
            return;
        }

        _chosedIndex = index;
        SetSelectedState(_chosedIndex);
    }
}
