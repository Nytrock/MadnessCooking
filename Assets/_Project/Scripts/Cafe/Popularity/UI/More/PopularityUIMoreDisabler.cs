using UnityEngine;
using UnityEngine.EventSystems;

public class PopularityUIMoreDisabler : MonoBehaviour, IPointerExitHandler {
    [SerializeField] private PopularityUIMore _moreUI;

    private RectTransform _rectTransform;

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (_rectTransform.ContainsLocalMouse())
            return;

        _moreUI.ChangeMode(false);
    }
}
