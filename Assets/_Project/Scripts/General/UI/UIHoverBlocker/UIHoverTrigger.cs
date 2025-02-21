using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class UIHoverTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private UIHoverListener _hoverListener;
    [SerializeField] private bool _isScrollBlocked;
    [SerializeField] private bool _isHoverReversed;
    [SerializeField] private bool _isDebug;

    private RectTransform _rect;

    private void Awake() {
        _rect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Enter();
    }

    public void OnPointerExit(PointerEventData eventData) {
        Leave();
    }

    public void OnDisable() {
        Leave();
    }

    public void OnEnable() {
        if (_rect.ContainsMouse())
            Enter();
    }

    private void Enter() {
        ChangeStates(!_isHoverReversed, _isScrollBlocked);
    }

    private void Leave() {
        ChangeStates(_isHoverReversed, false);
    }

    private void ChangeStates(bool isHover, bool isScrollBlocked) {
        _hoverListener.ChangeHoverState(isHover);
        _hoverListener.ChangeScrollBlockState(isScrollBlocked);

        if (_isDebug) {
            Debug.Log($"{name} change hover to {isHover}");
            Debug.Log($"{name} change scroll block to {isScrollBlocked}");
        }
    }
}
