using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class GeneralUpgradeData : ISaveable {
    [SerializeField, JsonProperty] private bool _isUpgradedTimeRenderer;

    public bool IsUpgradedTimeRenderer => _isUpgradedTimeRenderer;

    public GeneralUpgradeData() {
        _isUpgradedTimeRenderer = false;
    }

    public void ChangeTimeRenderer() {
        _isUpgradedTimeRenderer = true;
    }
}
