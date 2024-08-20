using System;
using UnityEngine;

[Serializable]
public class RealTimeManagerData {
    [SerializeField] private float _realTime;

    public RealTimeManagerData() {
        _realTime = 0;
    }

    public void UpdateRealTime() {
        _realTime += Time.unscaledDeltaTime;
    }
}
