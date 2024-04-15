using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour, IBindable<MainData>
{
    public static MoneyManager instance;

    [SerializeField] private int _moneyAmount = 0;
    private MainData _data;

    public int MoneyAmount => _moneyAmount;

    public event Action<int> MoneyChanged;

    private void Awake()
    {
        instance = this;
    }

    private void LateStart()
    {
        MoneyChanged?.Invoke(_moneyAmount);
    }

    public void ChangeMoney(int changeValue)
    {
        _moneyAmount += changeValue;
        _data.MoneyAmount = _moneyAmount;
        MoneyChanged?.Invoke(_moneyAmount);
    }

    public void Bind(MainData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty) {
            data.MoneyAmount = _moneyAmount;
            LateStart();
            return;
        }

        _moneyAmount = data.MoneyAmount;
        LateStart();
    }
}
