using Newtonsoft.Json;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class TechnicHolderData {
    [SerializeField, JsonProperty] private Technic _technic;
    [SerializeField, JsonProperty] private bool _isCooking;
    [SerializeField, JsonProperty] private bool _isRepairing;
    [SerializeField, JsonProperty] private float _nowStrength;
    [SerializeField, JsonProperty] private float _nowWaitTime;
    [SerializeField, JsonProperty] private float _needWaitTime;
    [SerializeField, JsonProperty] private Order _nowOrder;

    private KitchenUpgradeData _upgradeData;

    public bool IsCooking => _isCooking;
    public bool IsRepairing => _isRepairing;
    public float NowStrength => _nowStrength;
    public float NowWaitTime => _nowWaitTime;
    public float NeedWaitTime => _needWaitTime;
    public Order NowOrder => _nowOrder;
    public Technic Technic => _technic;

    public event Action CookStoped;
    public event Action RepairStoped;

    public TechnicHolderData(Technic technic) {
        if (technic == null)
            return;

        _technic = technic;
        _nowStrength = _technic.Strength;
        _nowWaitTime = 0f;
    }

    public void StartRepair() {
        _isRepairing = true;
        MoneyManager.Instance.ChangeMoney(-GetRepairPrice());
        _needWaitTime = _technic.TimeRepair * _upgradeData.TechnicRepairSpeed * GetBrokenCoef();
    }

    public int GetRepairPrice() {
        float brokenCoef = GetBrokenCoef();
        int priceRepair = Mathf.CeilToInt(_technic.PriceRepair * brokenCoef / _upgradeData.TechnicRepairPrice);
        return priceRepair;
    }

    private float GetBrokenCoef() {
        float brokenCoef = 1 - (_nowStrength / _technic.Strength);
        if (brokenCoef == 1)
            brokenCoef = 1.2f;
        return brokenCoef;
    }

    public void StartCook(Order order) {
        _isCooking = true;
        _nowOrder = order;
        _needWaitTime = _nowOrder.Food.TimeToCook / _upgradeData.TechnicCookSpeed;

        float strengthDecrease = Random.Range(1f, 2f) / _upgradeData.TechnicStrengthMultiplier;
        _nowStrength = Mathf.Max(_nowStrength - strengthDecrease, 0);
    }

    public void Update() {
        if (!_isCooking && !_isRepairing)
            return;

        _nowWaitTime += InGameTime.Instance.NormalizedDeltaTime;
        if (_isCooking)
            _nowOrder.UpdateProgress(_nowWaitTime / _needWaitTime);

        if (_nowWaitTime > _needWaitTime) {
            if (_isCooking)
                StopCook();
            else if (_isRepairing)
                StopRepair();
        }
    }

    private void StopRepair() {
        _isRepairing = false;
        _nowStrength = _technic.Strength;
        _nowWaitTime = 0f;
        RepairStoped?.Invoke();
    }

    private void StopCook() {
        _isCooking = false;
        CookStoped?.Invoke();
        _nowOrder.FinishCook();
        _nowOrder = null;
        _nowWaitTime = 0f;
    }

    public void EmergencyStopCook() {
        if (!_isCooking)
            return;

        _isCooking = false;
        CookStoped?.Invoke();
        _nowOrder = null;
        _nowWaitTime = 0f;
    }

    public void SetUpgradeData(KitchenUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }
}
