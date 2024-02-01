using UnityEngine;

public class OrdersUIOpener : MonoBehaviour
{
    [SerializeField] private OrdersUI _ordersUI;

    public void OnMouseDown()
    {
        _ordersUI.ChangeState();
    }
}
