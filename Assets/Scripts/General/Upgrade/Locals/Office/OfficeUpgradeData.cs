using System;
using UnityEngine;

[Serializable]
public class OfficeUpgradeData : LocalUpgradeData {
    [SerializeField] private float _internetDownloadSpeed = 1;
    [SerializeField] private bool _isInternetDownloadInstant;

    public float InternetDownloadSpeed => _internetDownloadSpeed;
    public bool IsInternetDownloadInstant => _isInternetDownloadInstant;

    public void ChangeInternetDownloadSpeed(CoefficientUpgrade coefficientUpgrade) {
        _internetDownloadSpeed = coefficientUpgrade.Coefficient;
    }

    public void ChangeInternetDownloadInstant() {
        _isInternetDownloadInstant = true;
    }
}
