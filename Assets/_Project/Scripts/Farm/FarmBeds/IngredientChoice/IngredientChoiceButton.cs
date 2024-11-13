using UnityEngine;

public class IngredientChoiceButton : ChoiceSimpleButton<Ingredient> {
    [SerializeField] private BedTypeImageStyleChanger _defaultStyleChanger;
    [SerializeField] private BedTypeImageStyleChanger _choosedStyleChanger;
    private BedType _bedType;

    public override void Setup(Ingredient item, int index, ChoiceSimpleUI<Ingredient> ui) {
        base.Setup(item, index, ui);
        _icon.sprite = Item.Icon;
        _button.onClick.AddListener(
            delegate { ui.Choice(index); }
        );
    }

    public void SetBedType(BedType bedType) {
        _bedType = bedType;
        UpdatePanelStyle();
    }

    public override void ChangeChoosedState() {
        base.ChangeChoosedState();
        UpdatePanelStyle();
    }

    private void UpdatePanelStyle() {
        if (_isChoosed)
            _choosedStyleChanger.UpdateStyle(_bedType);
        else
            _defaultStyleChanger.UpdateStyle(_bedType);
    }
}
