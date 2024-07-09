using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class TechnicHolderData {
    [SerializeField] private Technic _technic;
    [SerializeField] private bool _isCooking;
    [SerializeField] private bool _isRepairing;
    [SerializeField] private float _nowStrength;
    [SerializeField] private float _nowWaitTime;
    [SerializeField] private float _needWaitTime;
    [SerializeField] private Order _nowOrder;

    public bool IsCooking => _isCooking;
    public bool IsRepairing => _isRepairing;
    public float NowStrength => _nowStrength;
    public float NowWaitTime => _nowWaitTime;
    public float NeedWaitTime => _needWaitTime;
    public Order NowOrder => _nowOrder;

    public event Action CookStoped;
    public event Action RepairStoped;

    public TechnicHolderData(Technic technic) {
        _technic = technic;
        _nowStrength = technic.Strength;
    }

    public void StartRepair(KitchenUpgradeData upgradeData) {
        _isRepairing = true;
        _needWaitTime = _technic.TimeRepair / upgradeData.TechnicRepairSpeed;
    }

    public void StartCook(KitchenUpgradeData upgradeData, Order order) {
        _isCooking = true;
        _nowOrder = order;
        _nowWaitTime = 0f;
        _needWaitTime = _nowOrder.Food.TimeToCook / upgradeData.TechnicCookSpeed;

        int strengthDecrease = (int)(Random.Range(1f, 2f) / upgradeData.TechnicStrengthMultiplier);
        _nowStrength = Mathf.Max(_nowStrength - strengthDecrease, 0);
    }

    public void Update() {
        if (!_isCooking && !_isRepairing)
            return;

        _nowWaitTime += InGameTime.Instance.DeltaTime;
        if (_nowWaitTime > _needWaitTime) {
            if (_isCooking)
                StopCook();
            else if (_isRepairing)
                StopRepair();
        }
    }

    private void StopRepair() {
        RepairStoped?.Invoke();
        _isRepairing = false;
        _nowStrength = _technic.Strength;
    }

    private void StopCook() {
        CookStoped?.Invoke();
        _isCooking = false;
        _nowOrder.FinishCook();
        _nowOrder = null;
    }

    public void DisableTechnic() {
        if (!_isCooking)
            return;

        CookStoped?.Invoke();
        _isCooking = false;
        _nowOrder = null;
    }
}
