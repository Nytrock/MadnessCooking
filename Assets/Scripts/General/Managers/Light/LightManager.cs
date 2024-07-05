using UnityEngine;

public class LightManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private SubLightManager[] _subLightManagers;
    [SerializeField, Min(1)] private float _secondsToChangeColor = 1;
    private LightManagerData _data;
    private bool _isActivation = true;

    private void Awake() {
        _timeManager.DaytimeChanged += ChangeLight;
    }

    private void Update() {
        if (_data.IsChanging) {
            _data.Update();
            foreach (var light in _subLightManagers)
                light.UpdateMaterial();
        }
    }

    private void ChangeLight(Daytime newDaytime) {
        if (_isActivation) {
            _isActivation = false;
            foreach (var light in _subLightManagers)
                light.SetInitialLight(newDaytime);
            return;
        }

        _data.StartChange();
        foreach (var light in _subLightManagers)
            light.SetNewLight(newDaytime);
    }

    public void Bind(GeneralData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.LightManager = new(_subLightManagers.Length);
        _data = data.LightManager;
        _data.SetTimeStep(_secondsToChangeColor);

        for (int i = 0; i < _subLightManagers.Length; i++) {
            SubLightManagerData subData = _data.GetData(i);
            _subLightManagers[i].Bind(ref subData, isFileEmpty);
            _data.SetData(i, subData);
        }
    }
}
