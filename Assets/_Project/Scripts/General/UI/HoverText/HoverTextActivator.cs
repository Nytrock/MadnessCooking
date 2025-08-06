using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTextActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
    protected HoverTextPanel _hoverPanel;
    protected string _textToShow;
    private bool _mobileFlag;

    public void OnPointerEnter(PointerEventData eventData) {
        if (Application.isMobilePlatform)
            return;

        ShowText();
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (Application.isMobilePlatform)
            return;

        HideText();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (!Application.isMobilePlatform || _hoverPanel.IsHovered)
            return;

        _mobileFlag = true;
        ShowText();
    }

    private void Update() {
        if (!Application.isMobilePlatform)
            return;

        if (!_hoverPanel.IsHovered)
            return;

        if (_mobileFlag)
            _mobileFlag = false;
        else if (Input.GetMouseButtonDown(0))
            HideText();
    }

    protected virtual void ShowText() {
        _hoverPanel.ShowText(_textToShow);
    }

    protected virtual void HideText() {
        _hoverPanel.ChangeState(false);
    }

    public void SetHoverPanel(HoverTextPanel hoverText) {
        _hoverPanel = hoverText;
    }
}
