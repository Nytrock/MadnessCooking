using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class UIHoverTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private UIHoverListener _hoverListener;
    [SerializeField] private bool _isReversed = false;
    [SerializeField] private bool _isDebug = false;

    private RectTransform _rect;

    private void Awake() {
        _rect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        _hoverListener.HoverChange(!_isReversed);
        if (_isDebug)
            Debug.Log("Poiner enter");
    }

    public void OnPointerExit(PointerEventData eventData) {
        _hoverListener.HoverChange(_isReversed);
        if (_isDebug)
            Debug.Log("Poiner exit");
    }

    public void OnDisable() {
        _hoverListener.HoverChange(_isReversed);
        if (_isDebug)
            Debug.Log("Disabled");
    }

    public void OnEnable() {
        if (_rect.ContainsMouse())
            _hoverListener.HoverChange(!_isReversed);

        if (_isDebug)
            Debug.Log("Enabled");
    }
}
