using UnityEngine;

public class LightManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private Material _lightMaterial;
    [SerializeField, Min(1)] private float _secondsToChangeColor = 1;
    [SerializeField] private DaytimeLight[] _lights;
    private LightManagerData _data;

    public float SecondsToChangeColor => _secondsToChangeColor;

    private void Awake() {
        _timeManager.DaytimeChanged += ChangeLight;
    }

    private void Update() {
        if (_data.IsChanging) {
            _data.Update();
            UpdateMaterial();
        }
    }

    private void ChangeLight(Daytime newDaytime) {
        foreach (var light in _lights) {
            if (light.Daytime == newDaytime) {
                _data.StartChange(light);
                UpdateMaterial();
            }
        }
    }

    private void UpdateMaterial() {
        _lightMaterial.SetColor("_LightColor", _data.NowLight);
    }

    public void Bind(GeneralData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.LightManager = new();
        _data = data.LightManager;
        _data.SetTimeStep(_secondsToChangeColor);
    }
}
