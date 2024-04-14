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

    private void Start()
    {
        MoneyChanged?.Invoke(_moneyAmount);
    }

    public void ChangeMoney(int changeValue)
    {
        _moneyAmount += changeValue;
        _data.MoneyAmount = _moneyAmount;
        MoneyChanged?.Invoke(_moneyAmount);
    }

    public void Bind(MainData data)
    {
        _moneyAmount = data.MoneyAmount;
    }

    public void SetData(MainData data)
    {
        _data = data;
    }
}
