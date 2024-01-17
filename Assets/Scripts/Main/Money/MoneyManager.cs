using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    private int _moneyAmount = 0;

    public int MoneyAmount => _moneyAmount;

    public event Action<int> moneyChanged;

    private void Start()
    {
        instance = this;
    }

    public void ChangeMoney(int changeValue)
    {
        _moneyAmount += changeValue;
        moneyChanged?.Invoke(_moneyAmount);
    }
}
