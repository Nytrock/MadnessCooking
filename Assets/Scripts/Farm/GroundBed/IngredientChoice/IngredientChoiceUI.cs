using System.Collections.Generic;
using UnityEngine;

public class IngredientChoiceUI : ChoiceUI
{
    [SerializeField] private IngredientsManager _ingredientsManager;
    [SerializeField] private IngredientChoiceStyle[] _styles;
    private List<Ingredient> _ingredients;
    protected GameObject _stylePanel;
    private GroundBed _changingBed;

    protected override void Start()
    {
        DisableAllStyles();
        base.Start();
    }

    public void Activate(GroundBed groundBed)
    {
        _changingBed = groundBed;
        var bedType = _changingBed.BedType;
        GenerateChoiceButtons(bedType);
        SetStyle(bedType);
        Activate();
    }

    protected void GenerateChoiceButtons(BedType bedType)
    {
        _ingredients = _ingredientsManager.GetIngredientsOfOneBedType(bedType);
        for (int i = 0; i < _ingredients.Count; i++) {
            var choiceButton = Instantiate(_choiceButtonPrefab, _choiceButtonsContainer);
            choiceButton.GetComponent<IngredientChoiceButton>()
                .Setup(_ingredients[i], i, this);
            _choiceButtons.Add(choiceButton);
        }
    }

    private void SetStyle(BedType bedType)
    {
        foreach (var style in  _styles) {
            if (style.BedType == bedType) {
                style.Panel.SetActive(true);
                _stylePanel = style.Panel;
                _submitButton = style.MainButton;
                _submitButton.onClick.AddListener(SetChoice);
            }
        }
    }

    private void DisableAllStyles()
    {
        foreach (var style in _styles) {
            style.Panel.SetActive(false);
            style.MainButton.interactable = false;
        }
    }

    public override void SetChoice()
    {
        _changingBed.SetIngredient(_ingredients[_chosedIndex]);
        Deactivate();
        base.SetChoice();
    }

    private void Deactivate()
    {
        _submitButton.onClick.RemoveAllListeners();
        _stylePanel.SetActive(false);
    }
}
