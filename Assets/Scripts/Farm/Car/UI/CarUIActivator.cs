using UnityEngine;

public class CarUIActivator : UIActivator
{
    [SerializeField] private CarUI _carUI;

    protected override void Press()
    {
        _carUI.ChangePanelState();
    }
}
