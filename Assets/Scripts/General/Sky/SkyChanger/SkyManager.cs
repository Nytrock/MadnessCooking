using UnityEngine;

public class SkyManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private Material _material;
    [SerializeField] private LightManager _lightManager;
    [SerializeField] private DaytimeSky[] _skyes;
    private SkyManagerData _data;

    private void Awake() {
        _timeManager.DaytimeChanged += UpdateGradient;
    }

    private void Update() {
        if (_data.IsChanging) {
            _data.Update();
            UpdateMaterial();
        }
    }

    private void UpdateMaterial() {
        _material.SetColor("_TopColor", _data.NowTopColor);
        _material.SetColor("_BottomColor", _data.NowBottomColor);
    }

    private void UpdateGradient(Daytime newDaytime) {
        foreach (var sky in _skyes) {
            if (sky.Daytime == newDaytime) {
                _data.StartChange(sky.SkyGradient);
                UpdateMaterial();
            }
        }
    }

    public void Bind(GeneralData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.SkyManager = new();
        _data = data.SkyManager;
        _data.SetTimeStep(_lightManager.SecondsToChangeColor);
    }
}
