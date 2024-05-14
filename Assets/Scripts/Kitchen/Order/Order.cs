using System;
using UnityEngine;

[Serializable]
public class Order
{
    [SerializeField] private bool _isActivated;
    [SerializeField] private bool _isFinished;
    [SerializeField] private bool _isCooking;
    [SerializeField] private Food _food;
    [SerializeField] private int _tableNumber;

    public bool IsActivated => _isActivated;
    public bool IsFinished => _isFinished;
    public bool IsCooking => _isCooking;
    public Food Food => _food;
    public int TableNumber => _tableNumber;

    public event Action OrderFinished;

    public Order(Food food, int tableNumber)
    {
        _food = food;
        _tableNumber = tableNumber;
    }

    public void StartCook()
    {
        _isCooking = true;
    }

    public void FinishCook()
    {
        _isCooking = false;
        _isFinished = true;
        OrderFinished?.Invoke();
    }

    public void Activate()
    {
        _isActivated = true;
    }
}