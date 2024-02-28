using UnityEngine;
using UnityEngine.EventSystems;

public class PopularityUIMoreDisabler : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] private PopularityUIMore _moreUI;

    public void OnPointerExit(PointerEventData eventData)
    {
        _moreUI.ChangeMode(false);
    }
}
