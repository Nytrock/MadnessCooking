using System;
using UnityEngine;

[Serializable]
public class GeneralUpgradeData : LocalUpgradeData {
    [SerializeField] private bool _isUpgradedTimeRenderer;

    public bool IsUpgradedTimeRenderer => _isUpgradedTimeRenderer;

    public GeneralUpgradeData() {
        _isUpgradedTimeRenderer = false;
    }

    public void ChangeTimeRenderer() {
        _isUpgradedTimeRenderer = true;
    }
}
