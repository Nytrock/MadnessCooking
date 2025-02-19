using System;
using UnityEngine;

public class MoneyManager : Singleton<MoneyManager>, IBindable<GeneralData> {
    [SerializeField, Min(0)] private int _moneyDefault;
    private MoneyManagerData _data;

    public int MoneyCount => _data.MoneyCount;

    public event Action<int> MoneyChanged;

    public void LateStart() {
        MoneyChanged?.Invoke(_data.MoneyCount);
    }

    [ContextMenu(nameof(AddThousandMoney))]
    private void AddThousandMoney() {
        ChangeMoney(1000);
    }

    public void ChangeMoney(int changeValue) {
        if (changeValue == 0)
            return;

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
    }
}
