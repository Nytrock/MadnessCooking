using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTextActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    protected HoverTextPanel _hoverPanel;
    protected string _textToShow;

    public virtual void OnPointerEnter(PointerEventData eventData) {
        _hoverPanel.ShowText(_textToShow);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _hoverPanel.ChangeState(false);
    }

    public void SetHoverPanel(HoverTextPanel hoverText) {
        _hoverPanel = hoverText;
    }
}
