using UnityEngine;

public class OrderUIActivator : UIActivator 
{
    [SerializeField] private OrdersUI _ordersUI;

    protected override void Press()
    {
        _ordersUI.ChangeState();
    }
}
