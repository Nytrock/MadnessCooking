using UnityEngine;

public class OfficeDataLoader : LocalDataLoader<OfficeData> {
    [SerializeField, RequireInterface(typeof(IBindable<OfficeData>))]
    protected MonoBehaviour[] _officeBindableObjects;

    protected override void SetBindableObjects() {
        _bindableObjects = _officeBindableObjects;
    }
}
