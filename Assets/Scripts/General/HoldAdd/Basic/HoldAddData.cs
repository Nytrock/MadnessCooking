using System;
using UnityEngine;

[Serializable]
public class HoldAddData {
    [SerializeField] private float _nowTime;
    [SerializeField] private bool _isUnlocked;
    [SerializeField] private bool _isAuto;
    [SerializeField] private float _speed = 1;
    [SerializeField] private int _readyCount;

    public float NowTime => _nowTime;
    public bool IsUnlocked => _isUnlocked;
    public bool IsAuto => _isAuto;
    public float Speed => _speed;
    public int ReadyCount => _readyCount;

    public void ResetAll() {
        if (_isAuto)
            ResetTime();
    }

    public void ResetTime() {
        _nowTime = 0;
    }

    public void UpdateUpgrades(CoefficientUpgrade speedUpgrade) {
        if (_isAuto)
            _speed = speedUpgrade.Coefficient;
    }

    public void UpdateTime() {
        _nowTime += Time.deltaTime * _speed;
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

    public void MakeAuto() {
        _isAuto = true;
    }
}
