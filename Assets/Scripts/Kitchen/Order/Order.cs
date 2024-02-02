using System;

public class Order
{
    private bool _isFinished;
    private bool _isCooking;
    public int TableNumber;
    public Food Food;

    public bool IsFinished => _isFinished;
    public bool IsCooking => _isCooking;

    public void StartCook()
    {
        _isCooking = true;
    }

    public void FinishCook()
    {
        _isCooking = false;
        _isFinished = true;
    }
}