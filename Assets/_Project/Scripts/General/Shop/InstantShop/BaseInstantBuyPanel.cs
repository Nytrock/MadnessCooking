using UnityEngine;

namespace MadnessCooking.General {
    public class BaseInstantBuyPanel : BaseBuyPanel {

        [SerializeField] private ItemInfoRendererWithPrice _itemInfoRenderer;
        [SerializeField] protected string _priceDescription;

        public override void Setup(BuyPanelData data) {
            if (_data != null)
                MoneyManager.Instance.MoneyChanged -= UpdateButton;
            base.Setup(data);
            MoneyManager.Instance.MoneyChanged += UpdateButton;
        }

        public override void SetVisual() {
            _itemInfoRenderer.SetItemInfo(_data.Item);
            _itemInfoRenderer.SetPrice(_priceDescription, _data.Item.Price);
        }

        public override void SetSideInfo() {
            base.SetSideInfo();
            UpdateButton(MoneyManager.Instance.MoneyCount);
        }

        protected virtual void UpdateButton(int moneyCount) {
            _buyButton.interactable = moneyCount >= _data.Item.Price && _data.IsBuyable;
        }

        public override void Destroy() {
            MoneyManager.Instance.MoneyChanged -= UpdateButton;
            base.Destroy();
        }
    }
}
