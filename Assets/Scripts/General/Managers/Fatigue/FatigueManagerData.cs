using System;
using UnityEngine;

[Serializable]
public class FatigueManagerData {
    [SerializeField] private float _fatigue;

    public float Fatigue => _fatigue;

    public FatigueManagerData() {
        _fatigue = 0;
    }

    public void ChangeFatigue(float count, float fatigueMax) {
        _fatigue += count;

        if (_fatigue < 0)
            _fatigue = 0;
        if (_fatigue > fatigueMax)
            _fatigue = fatigueMax;
    }
}
