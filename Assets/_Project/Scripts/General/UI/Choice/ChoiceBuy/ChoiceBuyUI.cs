using UnityEngine;

namespace MadnessCooking.General {
    public abstract class ChoiceBuyUI<TItem, TButton> : ChoiceUI<TItem, TButton>
        where TItem : BuyableItem where TButton : ChoiceBuyButton<TItem> {

        [SerializeField] protected ChoiceBuyDescriptionUI _description;

        public override void SelectButton(TButton button) {
            base.SelectButton(button);
            _description.ChangeState(_choosedButton != null);
            _submitButton.interactable &= button.IsBuyable;

            if (_choosedButton == null)
                return;
            _description.UpdateDescription(_choosedButton.Item);
        }

        public override void SubmitChoice() {
            int price = _choosedButton.Item.Price;
            MoneyManager.Instance.ChangeMoney(-price);
        }

        protected override void Activate() {
            base.Activate();
            _description.ChangeState(false);
        }
    }
}
