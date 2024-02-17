using UnityEngine;
using System;

public class Order
{
    private bool _isFinished;
    private bool _isCooking;
    private Food _food;

    public int TableNumber;

    public bool IsFinished => _isFinished;
    public bool IsCooking => _isCooking;
    public Food Food => _food;

    public event Action OrderFinished;

    public Order (Food food, int tableNumber)
    {
        _food = food;
        TableNumber = tableNumber;
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
}