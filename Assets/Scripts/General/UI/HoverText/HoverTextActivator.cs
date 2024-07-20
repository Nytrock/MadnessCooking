using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTextActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private HoverText _hoverText;
    protected string _showingMessage;

    public void OnPointerEnter(PointerEventData eventData) {
        _hoverText.ShowText(_showingMessage);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _hoverText.ChangeState(false);
    }

    public void SetHoverText(HoverText hoverText) {
        _hoverText = hoverText;
    }
}
