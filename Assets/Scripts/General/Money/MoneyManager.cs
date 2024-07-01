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

    public void ChangeMoney(int changeValue) {
        _data.ChangeMoneyCount(changeValue);
        MoneyChanged?.Invoke(_data.MoneyCount);
    }

    public void Bind(GeneralData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.MoneyManager = new(_moneyDefault);
        _data = data.MoneyManager;
        LateStart();
    }
}
