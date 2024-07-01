using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class TechnicHolderData {
    [SerializeField] private bool _isCooking;
    [SerializeField] private bool _isRepairing;
    [SerializeField] private float _nowStrength;
    [SerializeField] private float _nowWaitTime;

    public bool IsCooking => _isCooking;
    public bool IsRepairing => _isRepairing;
    public float NowStrength => _nowStrength;
    public float NowWaitTime => _nowWaitTime;

    public TechnicHolderData(Technic technic) {
        _nowStrength = technic.Strength;
    }

    public void ChangeRepairState(bool newState) {
        _isRepairing = newState;
    }

    public void StartCooking(KitchenUpgradeData upgradeData) {
        _isCooking = true;

        int strengthDecrease = (int)(Random.Range(1f, 2f) / upgradeData.TechnicStrengthMultiplier);
        _nowStrength = Mathf.Max(_nowStrength - strengthDecrease, 0);
    }

    public void StopCooking() {
        _isCooking = false;
    }

    public void AddTime(float speedMultiplier) {
        _nowWaitTime += speedMultiplier * InGameTime.Instance.DeltaTime;
    }

    public void ResetTime() {
        _nowWaitTime = 0f;
    }
}
