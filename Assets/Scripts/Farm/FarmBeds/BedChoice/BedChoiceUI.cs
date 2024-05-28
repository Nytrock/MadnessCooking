using UnityEngine;

public class BedChoiceUI : ChoiceBuyWithCameraStopUI<BedType, FarmData>, IBindable<FarmData> {
    [SerializeField] private BedTypesManager _bedTypesManager;
    [SerializeField] private BedTypeIngredientsRenderer _ingredientsRenderer;
    private BedChoice _changingBed;

    private void Awake() {
        _bedTypesManager.TypeAdded += AddType;
    }

    private void LateStart() {
        GenerateChoiceButtons();
        base.Start();
    }

    public void ActivateBedChoice(BedChoice newBed) {
        _changingBed = newBed;
        Activate();
    }

    protected override void GenerateChoiceButtons() {
        for (int i = 0; i < _bedTypesManager.BedsCount; i++) {
            var choiceButton = _choiceButtonPool.GetObject() as BedChoiceButton;
            BedType bed = _bedTypesManager.GetBed(i);
            choiceButton.Setup(bed, i, this);
            choiceButton.SetBlockedState(!_bedTypesManager.HaveBed(bed));
            _choiceButtons.Add(choiceButton);
        }
    }

    private void AddType(BedType newType) {
        foreach (var button in _choiceButtons) {
            if (button.Item == newType) {
                var choiceButton = button as BedChoiceButton;
                choiceButton.SetBlockedState(false);
                break;
            }
        }
    }

    public override void Choice(int index, bool isBuyable) {
        base.Choice(index, isBuyable);

        if (_chosedIndex == -1)
            return;

        BedType bedType = _choiceButtons[_chosedIndex].Item;
        _ingredientsRenderer.ShowIngredients(bedType);
        _description.UpdateDescription(bedType);

        _submitButton.interactable &= _ingredientsRenderer.HaveIngredients(bedType);
    }

    public override void SetChoice() {
        base.SetChoice();
        _changingBed.SetType(_choiceButtons[_chosedIndex].Item);
        _changingBed = null;
        Disable();
    }

    protected override void SetSelectedState(int index) {
        _choiceButtons[index].ChangeSelectedState();
    }

    public override void Disable() {
        base.Disable();
        _changingBed = null;
    }

    public void Bind(FarmData data, bool isFileEmpty) {
        LateStart();
    }
}
