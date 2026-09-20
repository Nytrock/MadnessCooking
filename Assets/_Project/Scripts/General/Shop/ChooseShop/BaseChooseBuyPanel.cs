using UnityEngine;

namespace MadnessCooking.General {
    public class BaseChooseBuyPanel : BaseBuyPanel {
        [SerializeField] private BuyableItemRendererWithName _itemInfoRenderer;
        [SerializeField] protected GameObject _outline;

        public BuyableItem Item => _data.Item;

        protected virtual void Awake() {
            _outline.SetActive(false);
        }

        public override void SetVisual() {
            _itemInfoRenderer.SetItemInfo(_data.Item);
        }

        public virtual void UpdateSelection(bool isSelected) {
            _outline.SetActive(isSelected);
        }
    }
}
