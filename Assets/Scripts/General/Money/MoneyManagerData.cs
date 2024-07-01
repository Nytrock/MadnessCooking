using System;
using UnityEngine;

[Serializable]
public class MoneyManagerData {
    [SerializeField] private int _moneyCount;

    public int MoneyCount => _moneyCount;

    public MoneyManagerData(int moneyDefault) {
        _moneyCount = moneyDefault;
    }

    public void ChangeMoneyCount(int changeValue) {
        if (_moneyCount + changeValue < 0)
            return;

        _moneyCount += changeValue;
    }
}
