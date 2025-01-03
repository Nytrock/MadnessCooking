using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class FatigueManagerData {
    [SerializeField, JsonProperty] private float _fatigueNow;
    [SerializeField, JsonProperty] private float _fatigueMax;

    public float FatigueNow => _fatigueNow;

    public FatigueManagerData(float fatigueMax, float fatigueDefault) {
        _fatigueNow = Mathf.Clamp(fatigueDefault, 0, fatigueMax);
    }

    public void SetFatigueMax(float fatigueMax) {
        _fatigueMax = fatigueMax;
    }

    public void ChangeFatigue(float count) {
        _fatigueNow += count;

        if (_fatigueNow < 0)
            _fatigueNow = 0;
        if (_fatigueNow > _fatigueMax)
            _fatigueNow = _fatigueMax;
    }
}
