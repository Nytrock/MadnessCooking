using UnityEngine;

public class GameSettingsLoader : LocalDataLoader<GameSettingsData> {
    [SerializeField, RequireInterface(typeof(IBindable<GameSettingsData>))]
    protected MonoBehaviour[] _gameBindableObjects;

    protected override void SetBindableObjects() {
        _bindableObjects = _gameBindableObjects;
    }
}
