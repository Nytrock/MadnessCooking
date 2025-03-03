using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverItemNameActivator : HoverTextActivator {
    [SerializeField] protected Image _icon;
    protected BuyableItem _showingItem;

    public void SetItem(BuyableItem item) {
        _icon.sprite = item.Icon;
        _showingItem = item;
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        _hoverPanel.ShowText(_showingItem.Name);
    }
}
