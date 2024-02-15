using UnityEngine;
using UnityEngine.EventSystems;

public class PopularityUIMoreActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PopularityUIMore _moreUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _moreUI.ChangeMode(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _moreUI.ChangeMode(false);
    }
}
