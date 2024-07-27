using System;
using UnityEngine;

[Serializable]
public class OfficeUpgradeData : LocalUpgradeData {
    [SerializeField] private float _internetDownloadSpeed;
    [SerializeField] private bool _isInternetDownloadInstant;

    public float InternetDownloadSpeed => _internetDownloadSpeed;
    public bool IsInternetDownloadInstant => _isInternetDownloadInstant;

    public OfficeUpgradeData() {
        _internetDownloadSpeed = 1;
    }

    public void ChangeInternetDownloadSpeed(CoefficientUpgrade coefficientUpgrade) {
        _internetDownloadSpeed = coefficientUpgrade.Coefficient;
    }

    public void ChangeInternetDownloadInstant() {
        _isInternetDownloadInstant = true;
    }
}
