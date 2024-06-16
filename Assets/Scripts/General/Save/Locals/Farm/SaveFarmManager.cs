using UnityEngine;

public class SaveFarmManager : LocalSaveManager<FarmData> {
    [SerializeField, RequireInterface(typeof(IBindable<FarmData>))]
    protected MonoBehaviour[] _farmBindableObjects;

    protected override void SetBindableObjects() {
        _bindableObjects = _farmBindableObjects;
    }
}
