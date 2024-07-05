using System;
using UnityEngine;

[Serializable]
public class LightManagerData {
    [SerializeField] private float _nowTime;
    [SerializeField] private bool _isChanging;
    [SerializeField] private SubLightManagerData[] _subDatas;
    private float _timeStep;

    public bool IsChanging => _isChanging;

    public LightManagerData(int subLightsCount) {
        _subDatas = new SubLightManagerData[subLightsCount];
    }

    public SubLightManagerData GetData(int i) {
        return _subDatas[i];
    }

    public void SetData(int i, SubLightManagerData data) {
        _subDatas[i] = data;
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
        foreach (var subLight in _subDatas)
            subLight.UpdateMaterial(_nowTime);
    }
}
