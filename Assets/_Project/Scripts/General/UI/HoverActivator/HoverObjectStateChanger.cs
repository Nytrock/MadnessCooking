using UnityEngine;
using UnityEngine.EventSystems;

public abstract class HoverObjectStateChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
    private RectTransform _rectTransform;
    private bool _mobileFlag;
    protected bool _isActive;

    protected virtual void Awake() {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (Application.isMobilePlatform)
            return;

        ActivateHoverObject();
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (Application.isMobilePlatform || _rectTransform.ContainsLocalMouse())
            return;

        DisableHoverObject();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (!Application.isMobilePlatform || _isActive)
            return;

        _mobileFlag = true;
        ActivateHoverObject();
    }

    private void Update() {
        if (!Application.isMobilePlatform)
            return;

        if (!_isActive)
            return;

        if (_mobileFlag)
            _mobileFlag = false;
        else if (Input.GetMouseButtonDown(0))
            DisableHoverObject();
    }

    protected virtual void ActivateHoverObject() {
        _isActive = true;
    }

    protected virtual void DisableHoverObject() {
        _isActive = false;
    }
}
