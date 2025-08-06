using UnityEngine;
using UnityEngine.UI;

public class HoverItemNameActivator : HoverTextActivator {
    [SerializeField] protected Image _icon;
    protected BuyableItem _showingItem;

    public void SetItem(BuyableItem item) {
        _icon.sprite = item.Icon;
        _showingItem = item;
    }

    protected override void ShowText() {
        _hoverPanel.ShowText(_showingItem.Name);
    }
}
