using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class PopularityUIMoreActivator : MonoBehaviour, IPointerEnterHandler {
    [SerializeField] private PopularityUIMore _moreUI;

    public void OnPointerEnter(PointerEventData eventData) {
        _moreUI.ChangeMode(true);
    }
}
