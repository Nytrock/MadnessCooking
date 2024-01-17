using UnityEngine;

public class BedChoiceUI : ChoiceUI
{
    [SerializeField] private BedTypesManager _bedTypesManager;
    [SerializeField] private BedChoiceDescrUI _description;
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
        _description.Disable();
        Activate();
    }

    protected override void GenerateChoiceButtons()
    {
        for (int i = 0; i < _bedTypesManager.HaveBeds.Count; i++) {
            var choiceButton = Instantiate(_choiceButtonPrefab, _choiceButtonsContainer);
            choiceButton.GetComponent<BedChoiceButton>().Setup(_bedTypesManager.HaveBeds[i], i, this);
            _choiceButtons.Add(choiceButton);
        }
    }

    private void AddType(BedType newType)
    {
        foreach (var button in _choiceButtons)
            Destroy(button);
        _choiceButtons.Clear();
        GenerateChoiceButtons();
    }

    public void Choice(int index, bool isBuyable)
    {
        if (_chosedIndex == -1)
            _description.ChangeActive();
        else
            _choiceButtons[_chosedIndex].ChangeSelectedState();

        _submitButton.interactable = index != _chosedIndex && isBuyable;
        if (index == _chosedIndex)  {
            _chosedIndex = -1;
            _description.ChangeActive();
            return;
        }

        _chosedIndex = index;
        _choiceButtons[_chosedIndex].ChangeSelectedState();
        _description.UpdateDescription(_bedTypesManager.HaveBeds[_chosedIndex]);
    }

    public override void SetChoice()
    {
        _changingBed.SetType(_bedTypesManager.HaveBeds[_chosedIndex]);
        base.SetChoice();
    }
}
