using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour, IBindable<MainData>
{
    public static MoneyManager instance;

    [SerializeField] private int _moneyDefault;
    private MainData _data;

    public int MoneyCount => _data.MoneyCount;

    public event Action<int> MoneyChanged;

    private void Awake()
    {
        instance = this;
    }

    private void LateStart()
    {
        MoneyChanged?.Invoke(_moneyDefault);
    }

    public void ChangeMoney(int changeValue)
    {
        if (_data.MoneyCount + changeValue < 0)
            throw new ArgumentException("Incorrect value for changing money count.");

        _data.MoneyCount += changeValue;
        MoneyChanged?.Invoke(_data.MoneyCount);
    }

    public void Bind(MainData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty)
            data.MoneyCount = _moneyDefault;
        LateStart();
    }
}
