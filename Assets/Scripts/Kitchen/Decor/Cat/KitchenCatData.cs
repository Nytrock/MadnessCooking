using System;
using UnityEngine;

[Serializable]
public class KitchenCatData {
    [SerializeField] private float _nowTime;
    [SerializeField] private float _needTime;
    [SerializeField] private bool _isPetted;

    public bool IsPetted => _isPetted;
    public float NowTime => _nowTime;
    public float NeedTime => _needTime;

    public KitchenCatData(float needTime) {
        _nowTime = needTime;
        _needTime = needTime;
    }

    public void Update() {
        if (!_isPetted)
            return;

        _nowTime += InGameTime.Instance.NormalizedDeltaTime;
        if (_nowTime > _needTime)
            _isPetted = false;
    }

    public void Pet() {
        _isPetted = true;
        _nowTime = 0;
    }
}
