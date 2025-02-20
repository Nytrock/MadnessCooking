using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class KitchenUpgradeData : ISaveable {
    [SerializeField, JsonProperty] private bool _isAutoSpice;
    [SerializeField, JsonProperty] private bool _isWaterAvailable;
    [SerializeField, JsonProperty] private float _technicCookSpeed;
    [SerializeField, JsonProperty] private float _technicRepairSpeed;
    [SerializeField, JsonProperty] private float _technicStrength;

    public bool IsAutoSpice => _isAutoSpice;
    public bool IsWaterAvailable => _isWaterAvailable;
    public float TechnicCookSpeed => _technicCookSpeed;
    public float TechnicRepairSpeed => _technicRepairSpeed;
    public float TechnicStrengthMultiplier => _technicStrength;

    public KitchenUpgradeData() {
        _technicCookSpeed = 1;
        _technicRepairSpeed = 1;
        _technicStrength = 1;
    }

    public void SetAutoSpice() {
        _isAutoSpice = true;
    }

    public void ChangeWaterAvailable() {
        _isWaterAvailable = true;
    }

    public void ChangeTechnicCookSpeed(CoefficientUpgrade coefficientUpgrade) {
        _technicCookSpeed = coefficientUpgrade.Coefficient;
    }

    public void ChangeTechnicRepairSpeed(CoefficientUpgrade coefficientUpgrade) {
        _technicRepairSpeed = coefficientUpgrade.Coefficient;
    }

    public void ChangeTechnicStrength(CoefficientUpgrade coefficientUpgrade) {
        _technicStrength = coefficientUpgrade.Coefficient;
    }
}
