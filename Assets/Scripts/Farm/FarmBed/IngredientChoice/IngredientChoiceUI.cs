using System.Collections.Generic;
using UnityEngine;

public class IngredientChoiceUI : ChoiceUI<IngredientChoiceButton>
{
    [SerializeField] private IngredientsManager _ingredientsManager;
    [SerializeField] private IngredientChoiceStyle[] _styles;
    private List<Ingredient> _ingredients;
    protected GameObject _stylePanel;
    private FarmBed _changingBed;

    protected override void Start()
    {
        DisableAllStyles();
        base.Start();
    }

    public void Activate(FarmBed groundBed)
    {
        _changingBed = groundBed;
        var bedType = _changingBed.BedType;
        DestoyOldButtons();
        GenerateChoiceButtons();
        SetStyle(bedType);
        Activate();
    }

    private void DestoyOldButtons()
    {
        foreach (var button in _choiceButtons)
            Destroy(button.gameObject);
        _choiceButtons.Clear();
    }

    protected override void GenerateChoiceButtons()
    {
        _ingredients = _ingredientsManager.GetIngredientsOfOneBedType(_changingBed.BedType);
        for (int i = 0; i < _ingredients.Count; i++) {
            var choiceButton = Instantiate(_choiceButtonPrefab, _choiceButtonsContainer);
            choiceButton.Setup(_ingredients[i], i, this);
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
        _submitButton.onClick.RemoveListener(SetChoice);
        _stylePanel.SetActive(false);
    }

    protected override void SetSelectedState(int index)
    {
        _choiceButtons[index].ChangeSelectedState();
    }
}
