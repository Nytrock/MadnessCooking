using UnityEngine;

public class CafeDataLoader : LocalDataLoader<CafeData> {
    [SerializeField, RequireInterface(typeof(IBindable<CafeData>))]
    protected MonoBehaviour[] _cafeBindableObjects;

    protected override void SetBindableObjects() {
        _bindableObjects = _cafeBindableObjects;
    }
}
