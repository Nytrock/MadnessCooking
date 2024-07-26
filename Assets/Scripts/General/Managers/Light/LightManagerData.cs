using System;
using UnityEngine;

[Serializable]
public class LightManagerData {
    [SerializeField] private float _nowTime;
    [SerializeField] private bool _isChanging;
    [SerializeField] private SkyManagerData _skyData;
    [SerializeField] private SpritesManagerData _spritesData;
    private float _timeStep;

    public bool IsChanging => _isChanging;
    public SkyManagerData SkyData => _skyData;
    public SpritesManagerData SpritesData => _spritesData;

    public LightManagerData() {
        _skyData = new();
        _spritesData = new();
    }

    public void StartChange() {
        _isChanging = true;
        _nowTime = 0;
    }

    public void SetTimeStep(float timeChanging) {
        _timeStep = 1 / timeChanging;
    }

    public void Update() {
        _nowTime += _timeStep * InGameTime.Instance.DeltaTime;
        if (_nowTime >= 1)
            _isChanging = false;
        _skyData.UpdateMaterial(_nowTime);
        _spritesData.UpdateMaterial(_nowTime);
    }
}
