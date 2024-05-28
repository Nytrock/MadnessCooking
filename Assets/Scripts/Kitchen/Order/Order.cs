using System;
using UnityEngine;

[Serializable]
public class Order {
    [SerializeField] private bool _isActivated;
    [SerializeField] private bool _isFinished;
    [SerializeField] private bool _isCooking;
    [SerializeField] private Food _food;
    [SerializeField] private int _tableIndex;

    public bool IsActivated => _isActivated;
    public bool IsFinished => _isFinished;
    public bool IsCooking => _isCooking;
    public Food Food => _food;
    public int TableIndex => _tableIndex;

    public event Action OrderStarted;

    public event Action OrderFinished;

    public Order(Food food, int tableIndex) {
        _food = food;
        _tableIndex = tableIndex;
    }

    public void StartCook() {
        _isCooking = true;
        OrderStarted?.Invoke();
    }

    public void FinishCook() {
        _isCooking = false;
        _isFinished = true;
        OrderFinished?.Invoke();
    }

    public void Activate() {
        _isActivated = true;
    }
}