using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    [SerializeField] private int _moneyAmount = 0;

    public int MoneyAmount => _moneyAmount;

    public event Action<int> MoneyChanged;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MoneyChanged?.Invoke(_moneyAmount);
    }

    public void ChangeMoney(int changeValue)
    {
        _moneyAmount += changeValue;
        MoneyChanged?.Invoke(_moneyAmount);
    }
}
