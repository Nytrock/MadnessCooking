using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    private int _moneyAmount = 0;

    public int MoneyAmount => _moneyAmount;

    public event Action moneyChanged;

    private void Start()
    {
        Instance = this;
    }

    public void ChangeMoney(int changeValue)
    {
        _moneyAmount += changeValue;
        moneyChanged?.Invoke();
    }
}
