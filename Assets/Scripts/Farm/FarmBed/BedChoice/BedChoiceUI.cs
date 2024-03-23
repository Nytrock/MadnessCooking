using UnityEngine;

public class BedChoiceUI : ChoiceUI<BedChoiceButton>
{
    [SerializeField] private BedTypesManager _bedTypesManager;
    [SerializeField] private BedChoiceDescriptionUI _description;
    [SerializeField] private BedTypeIngredientsRenderer _ingredientsRenderer;
    private BedChoice _changingBed;

    protected override void Start()
    {
        _bedTypesManager.TypeAdded += AddType;
        GenerateChoiceButtons();
        base.Start();
    }

    public void Activate(BedChoice newBed)
    {
        _changingBed = newBed;
        _description.ChangeActive(false);
        Activate();
    }

    protected override void GenerateChoiceButtons()
    {
        for (int i = 0; i < _bedTypesManager.BedsCount; i++) {
            var choiceButton = Instantiate(_choiceButtonPrefab, _choiceButtonsContainer);
            var bed = _bedTypesManager.GetBed(i);
            choiceButton.Setup(bed, i, this);
            choiceButton.SetBlockedState(_bedTypesManager.HaveBed(bed));
            _choiceButtons.Add(choiceButton);
        }
    }

    private void AddType(BedType newType)
    {
        foreach (var button in _choiceButtons)
            button.Destroy();
        _choiceButtons.Clear();
        GenerateChoiceButtons();
    }

    public void Choice(int index, bool isBuyable)
    {
        if (_chosedIndex == -1)
            _description.ChangeActive();
        else
            _choiceButtons[_chosedIndex].ChangeSelectedState();

        var isSame = index == _chosedIndex;
        if (isSame)  {
            _chosedIndex = -1;
            _description.ChangeActive();
            return;
        }

        _chosedIndex = index;
        _choiceButtons[_chosedIndex].ChangeSelectedState();

        var bedType = _bedTypesManager.GetBed(_chosedIndex);
        _ingredientsRenderer.ShowIngredients(bedType);
        _description.UpdateDescription(bedType);

        _submitButton.interactable = !isSame && isBuyable && _ingredientsRenderer.HaveIngredients(bedType);
    }

    public override void SetChoice()
    {
        _changingBed.SetType(_bedTypesManager.GetBed(_chosedIndex));
        base.SetChoice();
    }

    protected override void SetSelectedState(int index)
    {
        _choiceButtons[index].ChangeSelectedState();
    }
}
