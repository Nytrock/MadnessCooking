using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class HoldAddData {
    [SerializeField, JsonProperty] private float _nowTime;
    [SerializeField, JsonProperty] private bool _isUnlocked;
    [SerializeField, JsonProperty] private bool _isAuto;
    [SerializeField, JsonProperty] private float _speed;
    [SerializeField, JsonProperty] private int _readyCount;
    [SerializeField] private float _waitTime;
    [SerializeField] private bool _isWork;

    public float NowTime => _nowTime;
    public bool IsUnlocked => _isUnlocked;
    public bool IsAuto => _isAuto;
    public int ReadyCount => _readyCount;
    public float WaitTime => _waitTime;
    public bool IsWork => _isWork;
    public float Speed => _speed;

    public HoldAddData(int readyCount) {
        _readyCount = readyCount;
        _speed = 1;
    }

    public void ResetAll() {
        _isWork = false;
        if (_isAuto)
            ResetTime();
    }

    public void ResetTime() {
        _nowTime = 0;
    }

    public void UpdateTime() {
        _nowTime += InGameTime.Instance.NormalizedDeltaTime * _speed;
    }

    public void AddReady() {
        ResetTime();
        _readyCount++;
    }

    public void SetReady(int count) {
        _readyCount = count;
    }

    public void SubtractReady() {
        if (_readyCount == 0)
            return;

        _readyCount--;
    }


    public void Unlock() {
        _isUnlocked = true;
    }

    public void MakeAuto(CoefficientUpgrade speedUpgrade) {
        _isAuto = true;
        _speed = speedUpgrade.Coefficient;
    }

    public void SetTimeWait(float timeWait) {
        _waitTime = timeWait;
    }

    public void ChangeWork(bool newValue) {
        _isWork = newValue;
        if (!_isWork)
            ResetTime();
    }
}
