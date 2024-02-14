using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UIHoverListener _hoverListener;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverListener.HoverChange(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverListener.HoverChange(false);
    }
}
