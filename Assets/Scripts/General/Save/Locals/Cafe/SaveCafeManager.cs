using UnityEngine;

public class SaveCafeManager : LocalSaveManager<CafeData> {
    [SerializeField, RequireInterface(typeof(IBindable<CafeData>))]
    protected MonoBehaviour[] _cafeBindableObjects;

    protected override void SetBindableObjects() {
        _bindableObjects = _cafeBindableObjects;
    }
}
