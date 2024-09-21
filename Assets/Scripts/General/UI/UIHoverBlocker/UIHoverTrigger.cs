using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private UIHoverListener _hoverListener;
    [SerializeField] private bool _isReversed = false;

    private void Awake() {
        if (_hoverListener == null)
            Debug.Log(name);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        _hoverListener.HoverChange(!_isReversed);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _hoverListener.HoverChange(_isReversed);
    }

    public void OnDisable() {
        _hoverListener.HoverChange(_isReversed);
    }
}
