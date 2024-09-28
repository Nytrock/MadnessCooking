using System;
using UnityEngine;

public class MoneyManager : Singleton<MoneyManager>, IBindable<GeneralData> {
    [SerializeField, Min(0)] private int _moneyDefault;
    private MoneyManagerData _data;

    public int MoneyCount => _data.MoneyCount;

    public event Action<int> MoneyChanged;

    private void LateStart() {
        MoneyChanged?.Invoke(_moneyDefault);
    }

    [ContextMenu("TestMoney")]
    private void TestMoney() {
        ChangeMoney(10);
    }

    public void ChangeMoney(int changeValue) {
        _data.ChangeMoneyCount(changeValue);
        MoneyChanged?.Invoke(_data.MoneyCount);
    }

    public void ResetMoney() {
        _data.ChangeMoneyCount(-_data.MoneyCount);
        MoneyChanged?.Invoke(0);
    }

    public void Bind(GeneralData data) {
        data.MoneyManager ??= new(_moneyDefault);
        _data = data.MoneyManager;
        LateStart();
    }
}
