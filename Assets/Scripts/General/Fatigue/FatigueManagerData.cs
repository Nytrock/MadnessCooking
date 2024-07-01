using System;
using UnityEngine;

[Serializable]
public class FatigueManagerData {
    [SerializeField] private float _fatigue = 0;

    public float Fatigue => _fatigue;

    public void ChangeFatigue(float count, float fatigueMax) {
        _fatigue += count;

        if (_fatigue < 0)
            _fatigue = 0;
        if (_fatigue > fatigueMax)
            _fatigue = fatigueMax;
    }
}
