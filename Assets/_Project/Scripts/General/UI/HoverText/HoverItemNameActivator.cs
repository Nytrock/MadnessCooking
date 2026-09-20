using UnityEngine;
using UnityEngine.UI;

namespace MadnessCooking.General {
    public class HoverItemNameActivator : HoverTextActivator {
        [SerializeField] protected Image _icon;
        protected BuyableItem _showingItem;

        public void SetItem(BuyableItem item) {
            _icon.sprite = item.Icon;
            _showingItem = item;
        }

        protected override async void ShowText() {
            _hoverPanel.ShowText(await _showingItem.GetName());
        }
    }
}
