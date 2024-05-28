using System;
using UnityEngine;

public class MoneyManager : Singleton<MoneyManager>, IBindable<GeneralData> {
    [SerializeField, Min(0)] private int _moneyDefault;
    private GeneralData _data;

    public int MoneyCount => _data.MoneyCount;

    public event Action<int> MoneyChanged;

    private void LateStart() {
        MoneyChanged?.Invoke(_moneyDefault);
    }

    public void ChangeMoney(int changeValue) {
        if (_data.MoneyCount + changeValue < 0)
            throw new ArgumentException("Incorrect value for changing money count.");

        _data.MoneyCount += changeValue;
        MoneyChanged?.Invoke(_data.MoneyCount);
    }

    public void Bind(GeneralData data, bool isFileEmpty) {
        _data = data;
        if (isFileEmpty)
            data.MoneyCount = _moneyDefault;
        LateStart();
    }
}
