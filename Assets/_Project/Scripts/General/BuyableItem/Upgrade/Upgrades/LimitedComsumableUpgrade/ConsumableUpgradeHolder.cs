using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class ConsumableUpgradeHolder {
    [SerializeField, JsonProperty] private ConsumableUpgrade _consumableUpgrade;
    [SerializeField, Min(0), JsonProperty] private int _nowCount;

    public ConsumableUpgrade ConsumableUpgrade => _consumableUpgrade;
    public bool IsMax => _consumableUpgrade.MaxCount <= _nowCount;

    public ConsumableUpgradeHolder(ConsumableUpgrade upgrade) {
        _consumableUpgrade = upgrade;
        _nowCount = 0;
    }

    public void AddCount() {
        _nowCount++;
    }
}
