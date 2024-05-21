using System;
using UnityEngine;

[Serializable]
public class LimitedConsumableUpgradeHolder
{
    [SerializeField] private LimitedConsumableUpgrade _consumableUpgrade;
    [SerializeField] private int _nowCount;

    public LimitedConsumableUpgrade ConsumableUpgrade => _consumableUpgrade;
    public bool IsMax => _consumableUpgrade.MaxCount <= _nowCount;

    public void AddCount()
    {
        _nowCount++;
    }
}
