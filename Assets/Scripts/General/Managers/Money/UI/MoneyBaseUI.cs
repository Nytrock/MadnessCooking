using UnityEngine;

public abstract class MoneyBaseUI : MonoBehaviour {
    [SerializeField] private MoneyManager _moneyManager;
    protected string _countConverted;
    protected int _oldCount;
    protected bool _isCountAdded;

    protected virtual void Awake() {
        _moneyManager.MoneyChanged += UpdateCount;
        _oldCount = 0;
    }

    private void UpdateCount(int newCount) {
        _countConverted = CountConverter.ConvertCount(newCount, true);
        _isCountAdded = newCount > _oldCount;

        StartAnimation();
        _oldCount = newCount;
    }

    protected abstract void StartAnimation();
}
