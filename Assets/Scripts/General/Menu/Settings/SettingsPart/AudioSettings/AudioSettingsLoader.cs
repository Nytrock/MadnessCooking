using UnityEngine;

public class AudioSettingsLoader : LocalDataLoader<AudioSettingsData> {
    [SerializeField, RequireInterface(typeof(IBindable<AudioSettingsData>))]
    protected MonoBehaviour[] _audioBindableObjects;

    protected override void SetBindableObjects() {
        _bindableObjects = _audioBindableObjects;
    }
}
