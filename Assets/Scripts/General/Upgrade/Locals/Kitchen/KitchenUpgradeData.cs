using System;
using UnityEngine;

[Serializable]
public class KitchenUpgradeData : LocalUpgradeData {
    [SerializeField] private bool _isAutoSpice;
    [SerializeField] private bool _isStrengthShow;
    [SerializeField] private float _technicCookSpeed = 1;
    [SerializeField] private float _technicRepairSpeed = 1;
    [SerializeField] private float _technicStrength = 1;

    public bool IsAutoSpice => _isAutoSpice;
    public bool IsStrengthShow => _isStrengthShow;
    public float TechnicCookSpeed => _technicCookSpeed;
    public float TechnicRepairSpeed => _technicRepairSpeed;
    public float TechnicStrengthMultiplier => _technicStrength;

    public void SetAutoSpice() {
        _isAutoSpice = true;
    }

    public void ChangeStrengthShow() {
        _isStrengthShow = true;
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
