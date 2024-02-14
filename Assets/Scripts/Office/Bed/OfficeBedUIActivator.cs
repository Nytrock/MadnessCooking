using UnityEngine;

public class OfficeBedUIActivator : UIActivator
{
    [SerializeField] private OfficeBedUI _bedUI;

    protected override void Press()
    {
        _bedUI.ChangeState();
    }
}
