using UnityEngine;

public class FarmCarUIActivator : UIActivator
{
    [SerializeField] private FarmCarUI _carUI;

    protected override void Press()
    {
        _carUI.ChangePanelState();
    }
}
