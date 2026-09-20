using UnityEngine;
using UnityEngine.Events;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class IngredientChoiceButton : ChoiceButton<Ingredient> {
        [SerializeField] private BedTypeImageStyleChanger _defaultStyleChanger;
        [SerializeField] private BedTypeImageStyleChanger _choosedStyleChanger;
        private BedType _bedType;

        public override void Setup(Ingredient item, UnityAction buttonEvent) {
            base.Setup(item, buttonEvent);
            _icon.sprite = Item.Icon;
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
}
