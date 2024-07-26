using UnityEngine;

public class GeneralDataLoader : LocalDataLoader<GeneralData> {
    [SerializeField, RequireInterface(typeof(IBindable<GeneralData>))]
    protected MonoBehaviour[] _generalBindableObjects;

    protected override void SetBindableObjects() {
        _bindableObjects = _generalBindableObjects;
    }
}
