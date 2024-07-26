using UnityEngine;

public class KitchenDataLoader : LocalDataLoader<KitchenData> {
    [SerializeField, RequireInterface(typeof(IBindable<KitchenData>))]
    protected MonoBehaviour[] _kithenBindableObjects;

    protected override void SetBindableObjects() {
        _bindableObjects = _kithenBindableObjects;
    }
}
