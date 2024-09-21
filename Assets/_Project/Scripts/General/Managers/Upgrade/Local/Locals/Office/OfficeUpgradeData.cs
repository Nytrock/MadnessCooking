using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class OfficeUpgradeData : ISaveable {
    [SerializeField, JsonProperty] private float _internetDownloadSpeed;
    [SerializeField, JsonProperty] private bool _isInternetDownloadInstant;

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
