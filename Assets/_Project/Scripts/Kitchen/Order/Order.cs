using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class Order {
    [SerializeField, JsonProperty] private bool _isActivated;
    [SerializeField, JsonProperty] private bool _isFinished;
    [SerializeField, JsonProperty] private bool _isCooking;
    [SerializeField, JsonProperty] private Food _food;
    [SerializeField, JsonProperty] private int _tableIndex;

    private float _cookProgress = 0;

    public bool IsActivated => _isActivated;
    public bool IsFinished => _isFinished;
    public bool IsCooking => _isCooking;
    public Food Food => _food;
    public int TableIndex => _tableIndex;
    public float CookProgress => _cookProgress;

    public event Action OrderStarted;

    public event Action OrderFinished;

    public void Setup(Food food, int tableIndex) {
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

    public void UpdateProgress(float progress) {
        _cookProgress = progress;
    }
}