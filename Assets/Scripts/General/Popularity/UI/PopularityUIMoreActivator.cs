using UnityEngine;
using UnityEngine.EventSystems;

public class PopularityUIMoreActivator : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private PopularityUIMore _moreUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _moreUI.ChangeMode(true);
    }
}
