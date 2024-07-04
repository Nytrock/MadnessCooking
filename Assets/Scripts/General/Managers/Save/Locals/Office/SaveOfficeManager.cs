using UnityEngine;

public class SaveOfficeManager : LocalSaveManager<OfficeData> {
    [SerializeField, RequireInterface(typeof(IBindable<OfficeData>))]
    protected MonoBehaviour[] _officeBindableObjects;

    protected override void SetBindableObjects() {
        _bindableObjects = _officeBindableObjects;
    }
}
