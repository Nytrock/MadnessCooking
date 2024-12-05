using UnityEngine;
using UnityEngine.EventSystems;

public class PopularityUIMoreActivator : MonoBehaviour, IPointerEnterHandler {
    [SerializeField] private PopularityUIMore _moreUI;
    [SerializeField] private UIHoverListener _hoverListener;

    public void OnPointerEnter(PointerEventData eventData) {
        _moreUI.ChangeMode(true);
    }
}
