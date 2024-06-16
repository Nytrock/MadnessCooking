using System;
using UnityEngine;

[Serializable]
public class GeneralUpgradeData : LocalUpgradeData {
    [SerializeField] private bool _isUpgradedTimeRenderer = false;

    public bool IsUpgradedTimeRenderer => _isUpgradedTimeRenderer;

    public void ChangeTimeRenderer() {
        _isUpgradedTimeRenderer = true;
    }
}
