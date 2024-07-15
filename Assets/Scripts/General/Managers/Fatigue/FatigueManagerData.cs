using System;
using UnityEngine;

[Serializable]
public class FatigueManagerData {
    [SerializeField] private float _fatigueNow;
    [SerializeField] private float _fatigueMax;

    public float FatigueNow => _fatigueNow;

    public FatigueManagerData(float fatigueMax, float fatigueDefault) {
        _fatigueMax = fatigueMax;
        _fatigueNow = Mathf.Clamp(fatigueDefault, 0, fatigueMax);
    }

    public void ChangeFatigue(float count) {
        _fatigueNow += count;

        if (_fatigueNow < 0)
            _fatigueNow = 0;
        if (_fatigueNow > _fatigueMax)
            _fatigueNow = _fatigueMax;
    }
}
