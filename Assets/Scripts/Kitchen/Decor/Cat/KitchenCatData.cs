using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class KitchenCatData {
    [SerializeField, JsonProperty] private float _nowTime;
    [SerializeField, JsonProperty] private float _needTime;
    [SerializeField, JsonProperty] private bool _isPetted;

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
