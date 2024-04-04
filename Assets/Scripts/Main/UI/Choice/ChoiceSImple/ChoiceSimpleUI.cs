public abstract class ChoiceSimpleUI<T> : ChoiceUI<T, ChoiceSimpleButton<T>>
{
    public void Choice(int index)
    {
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
