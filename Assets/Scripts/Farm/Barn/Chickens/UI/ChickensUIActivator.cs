using UnityEngine;

public class ChickensUIActivator : UIActivator
{
    [SerializeField] private ChickensUI _chickensUI;

    protected override void Press()
    {
        _chickensUI.ChangeState();
    }
}
