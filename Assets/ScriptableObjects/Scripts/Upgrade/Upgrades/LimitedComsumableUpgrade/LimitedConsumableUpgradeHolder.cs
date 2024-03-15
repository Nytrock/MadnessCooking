using System;
using UnityEngine;

[Serializable]
public class LimitedConsumableUpgradeHolder
{
    [SerializeField] private LimitedConsumableUpgrade _consumableUpgrade;
    public int NowCount { get; private set; }

    public LimitedConsumableUpgrade ConsumableUpgrade => _consumableUpgrade;
    public bool IsMax => _consumableUpgrade.MaxCount <= NowCount;

    public void AddCount()
    {
        NowCount++;
    }
}
