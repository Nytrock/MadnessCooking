using UnityEngine;

public class VideoSettingsLoader : LocalDataLoader<VideoSettingsData> {
    [SerializeField, RequireInterface(typeof(IBindable<VideoSettingsData>))]
    protected MonoBehaviour[] _videoBindableObjects;

    protected override void SetBindableObjects() {
        _bindableObjects = _videoBindableObjects;
    }
}
