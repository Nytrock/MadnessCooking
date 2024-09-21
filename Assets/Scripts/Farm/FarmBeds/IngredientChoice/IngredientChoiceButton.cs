using UnityEngine;

[RequireComponent(typeof(BedTypeImageStyleChanger))]
public class IngredientChoiceButton : ChoiceSimpleButton<Ingredient> {
    private BedTypeImageStyleChanger _styleChanger;

    public override void Setup(Ingredient item, int index, ChoiceSimpleUI<Ingredient> ui) {
        base.Setup(item, index, ui);
        _icon.sprite = Item.Icon;
        _button.onClick.AddListener(
            delegate { ui.Choice(index); }
        );
    }

    public void UpdateStyle(BedType bedType) {
        if (_styleChanger == null)
            GetStyleChanger();

        _styleChanger.UpdateStyle(bedType);
    }

    private void GetStyleChanger() {
        _styleChanger = GetComponent<BedTypeImageStyleChanger>();
    }
}
