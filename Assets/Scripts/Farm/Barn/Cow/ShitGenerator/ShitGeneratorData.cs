using System;
using UnityEngine;

[Serializable]
public class ShitGeneratorData {
    [SerializeField] private float _nowTime = 0;

    public float NowTime => _nowTime;

    public void AddTime() {
        _nowTime += InGameTime.Instance.DeltaTime;
    }

    public void ResetTime() {
        _nowTime = 0;
    }
}
