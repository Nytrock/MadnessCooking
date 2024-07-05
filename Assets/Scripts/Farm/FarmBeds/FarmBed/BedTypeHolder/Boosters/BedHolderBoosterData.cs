using System;
using UnityEngine;

[Serializable]
public class BedHolderBoosterData {
    [SerializeField] private float _boost;
    [SerializeField] private bool _isBoosting;
    [SerializeField] private bool _isEternal;
    [SerializeField] private float _nowTime;

    public float Boost => _boost;
    public bool IsBoosting => _isBoosting;
    public bool IsEternal => _isEternal;
    public float NowTime => _nowTime;

    public BedHolderBoosterData() {
        _boost = 1;
    }

    public void StartBoost(float boost) {
        _isBoosting = true;
        _nowTime = 0;
        _boost = boost;
    }

    public void EndBoost(float defaultSpeed) {
        _isBoosting = false;
        _boost = defaultSpeed;
    }

    public void UpdateTime() {
        _nowTime += InGameTime.Instance.DeltaTime;
    }

    public void SetBoost(int boost) {
        _boost = boost;
    }

    public void BecomeEternal() {
        _isEternal = true;
    }

    public void DisableUpgrades() {
        _isEternal = false;
    }
}
