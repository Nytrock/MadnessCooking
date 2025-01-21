using UnityEngine;

public class BedChoiceUI : ChoiceBuyUI<BedType>, IBindable<FarmData> {
    [SerializeField] private BedTypeManager _bedTypesManager;
    [SerializeField] private BedTypeIngredientsRenderer _ingredientsRenderer;
    [SerializeField] private TutorialManager _tutorialManager;
    private BedChoice _changingBed;

    private void Awake() {
        _bedTypesManager.ItemAdded += AddType;
    }

    public void LateStart() {
        GenerateChoiceButtons();
        base.Start();
    }

    public void ActivateBedChoice(BedChoice newBed) {
        if (_tutorialManager.IsWork)
            _tutorialManager.NextTutorialPart();

        _changingBed = newBed;
        Activate();
    }

    protected override void GenerateChoiceButtons() {
        int index = 0;
        foreach (var bed in _bedTypesManager.GetAllItems()) {
            var choiceButton = _choiceButtonPool.GetObject() as BedChoiceButton;
            choiceButton.Setup(bed, index++, this);
            choiceButton.SetBlockedState(!_bedTypesManager.IsItemAvailable(bed));
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

        if (_tutorialManager.IsWork)
            _tutorialManager.NextTutorialPart();

        BedType bedType = _choiceButtons[_chosedIndex].Item;
        _ingredientsRenderer.ShowIngredients(bedType);
        _description.UpdateDescription(bedType);

        _submitButton.interactable &= _ingredientsRenderer.HaveIngredients(bedType);
    }

    public override void SetChoice() {
        base.SetChoice();
        if (_tutorialManager.IsWork)
            _tutorialManager.NextTutorialPart();

        _changingBed.SetType(_choiceButtons[_chosedIndex].Item);
        _changingBed = null;
        Disable();
    }

    protected override void SetSelectedState(int index) {
        _choiceButtons[index].ChangeChoosedState();
    }

    public override void Disable() {
        base.Disable();
        _changingBed = null;
    }

    public void Bind(FarmData data) { }
}
