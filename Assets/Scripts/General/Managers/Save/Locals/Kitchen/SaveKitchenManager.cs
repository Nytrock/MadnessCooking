using UnityEngine;

public class SaveKitchenManager : LocalSaveManager<KitchenData> {
    [SerializeField, RequireInterface(typeof(IBindable<KitchenData>))]
    protected MonoBehaviour[] _kithenBindableObjects;

    protected override void SetBindableObjects() {
        _bindableObjects = _kithenBindableObjects;
    }
}
