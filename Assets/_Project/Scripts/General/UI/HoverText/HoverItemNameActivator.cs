using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverItemNameActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] protected Image _icon;
    private HoverItemName _hoverText;
    protected BuyableItem _showingItem;

    public void SetItem(BuyableItem item) {
        _icon.sprite = item.Icon;
        _showingItem = item;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        _hoverText.ShowItemName(_showingItem);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _hoverText.ChangeState(false);
    }

    public void SetHoverText(HoverItemName hoverText) {
        _hoverText = hoverText;
    }
}
