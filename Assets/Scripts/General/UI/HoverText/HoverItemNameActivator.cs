using UnityEngine;
using UnityEngine.EventSystems;

public class HoverItemNameActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private HoverItemName _hoverText;
    protected BuyableItem _showingItem;

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
