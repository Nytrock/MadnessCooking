using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IngredientChoiceRenderer))]
public class IngredientChoiceUI : ChoiceSimpleUI<Ingredient> {
    [SerializeField] private IngredientsManager _ingredientsManager;

    private readonly List<Ingredient> _ingredients = new();
    private IngredientChoiceRenderer _renderer;
    private FarmBed _changingBed;

    private void Awake() {
        _renderer = GetComponent<IngredientChoiceRenderer>();
    }

    public void ActivateIngredientChoice(FarmBed farmBed) {
        _changingBed = farmBed;
        BedType bedType = _changingBed.Data.BedType;
        _renderer.UpdateStyle(bedType);
        DestoyOldButtons();
        GenerateChoiceButtons();
        Activate();
    }

    private void DestoyOldButtons() {
        foreach (var button in _choiceButtons)
            _choiceButtonPool.PutObject(button);
        _choiceButtons.Clear();
    }

    protected override void GenerateChoiceButtons() {
        _ingredients.Clear();
        foreach (var ingredient in _ingredientsManager.GetAvailableIngredientsOfBedType(_changingBed.Data.BedType)) {
            IngredientChoiceButton choiceButton = (IngredientChoiceButton)_choiceButtonPool.GetObject();
            choiceButton.Setup(ingredient, _ingredients.Count, this);
            _renderer.SetButtonStyle(choiceButton);
            _choiceButtons.Add(choiceButton);
            _ingredients.Add(ingredient);
        }
    }

    public override void SetChoice() {
        _changingBed.SetIngredient(_ingredients[_chosedIndex]);
        Disable();
    }

    protected override void SetSelectedState(int index) {
        _choiceButtons[index].ChangeChoosedState();
    }

    public override void Disable() {
        base.Disable();
        _changingBed = null;
    }
}
