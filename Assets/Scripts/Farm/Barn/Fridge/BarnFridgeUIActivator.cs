using UnityEngine;

public class BarnFridgeUIActivator : UIActivator
{
    [SerializeField] private BarnFridgeUI _fridgeUI;

    protected override void Press()
    {
        _fridgeUI.ChangeState();
    }
}
