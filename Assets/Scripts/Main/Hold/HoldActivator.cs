using UnityEngine;
using UnityEngine.EventSystems;

public class HoldActivator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private HoldAdd _hold;

    public void OnPointerDown(PointerEventData eventData)
    {
        _hold.ChangeWorkMode(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hold.ChangeWorkMode(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _hold.ChangeWorkMode(false);
    }
}
