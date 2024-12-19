using UnityEngine;

public class LightManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField] private GameTimeManager _timeManager;
    [SerializeField] private SkyManager _skyManager;
    [SerializeField] private SpritesManager _spritesManager;
    [SerializeField, Min(1)] private float _secondsToChangeColor = 1;
    [SerializeField] private LightManagerData _data;
    private bool _isActivation = true;

    private void Awake() {
        _timeManager.DaytimeChanged += ChangeLight;
    }

    private void Update() {
        if (_data.IsChanging) {
            _data.Update();
            _spritesManager.UpdateMaterial();
            _skyManager.UpdateMaterial();
        }
    }

    private void ChangeLight(Daytime newDaytime) {
        if (_isActivation) {
            _isActivation = false;
            _spritesManager.SetInitialLight(newDaytime);
            _skyManager.SetInitialLight(newDaytime);
            return;
        }

        _data.StartChange();
        _spritesManager.SetNewLight(newDaytime);
        _skyManager.SetNewLight(newDaytime);
    }

    public void LateStart() { }

    public void Bind(GeneralData data) {
        data.LightManager ??= new();
        _data = data.LightManager;

        _data.SetTimeStep(_secondsToChangeColor);
        _skyManager.Bind(_data.SkyData);
        _spritesManager.Bind(_data.SpritesData);
    }
}
