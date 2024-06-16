using UnityEngine;

public class SaveGeneralManager : LocalSaveManager<GeneralData> {
    [SerializeField, RequireInterface(typeof(IBindable<GeneralData>))]
    protected MonoBehaviour[] _generalBindableObjects;

    protected override void SetBindableObjects() {
        _bindableObjects = _generalBindableObjects;
    }
}
