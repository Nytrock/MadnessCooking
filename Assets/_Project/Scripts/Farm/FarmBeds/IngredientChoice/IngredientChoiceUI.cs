using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BedTypeStyleUpdater))]
public class IngredientChoiceUI : ChoiceSimpleUI<Ingredient> {
    [SerializeField] private IngredientsManager _ingredientsManager;
    [SerializeField] private TutorialManager _tutorialManager;

    private readonly List<Ingredient> _ingredients = new();
    private BedTypeStyleUpdater _renderer;
    private FarmBed _changingBed;

    private void Awake() {
        _renderer = GetComponent<BedTypeStyleUpdater>();
    }

    public void ActivateIngredientChoice(FarmBed farmBed) {
        if (_tutorialManager.IsWork)
            _tutorialManager.NextTutorialPart();

        _changingBed = farmBed;
        _renderer.UpdateStyle(_changingBed.Data.BedType);
        GenerateChoiceButtons();
        Activate();
    }

    protected override void GenerateChoiceButtons() {
        DestoyOldButtons();
        _ingredients.Clear();

        foreach (var ingredient in _ingredientsManager.GetAvailableIngredientsOfBedType(_changingBed.Data.BedType)) {
            IngredientChoiceButton choiceButton = (IngredientChoiceButton)_choiceButtonPool.GetObject();
            choiceButton.Setup(ingredient, _ingredients.Count, this);
            choiceButton.SetBedType(_changingBed.Data.BedType);
            _choiceButtons.Add(choiceButton);
            _ingredients.Add(ingredient);
        }
    }

    public override void SetChoice() {
        _changingBed.SetIngredient(_ingredients[_chosedIndex]);
        Disable();
    }

    protected override void SetSelectedState(int index) {
        if (_tutorialManager.IsWork)
            _tutorialManager.NextTutorialPart();

        _choiceButtons[index].ChangeChoosedState();
    }

    public override void Disable() {
        base.Disable();
        _changingBed = null;
    }
}
