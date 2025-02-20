using AYellowpaper;
using UnityEngine;


public class UIActivator : ColliderActivator {
    [SerializeField] protected InterfaceReference<IActivable> _activableObject;
    [SerializeField] private UIActivatorsManager _UIManager;

    protected override void Press() {
        _UIManager.SetActivable(_activableObject.Value);
    }
}
