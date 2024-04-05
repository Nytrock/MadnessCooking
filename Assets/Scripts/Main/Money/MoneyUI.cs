using UnityEngine;

public class MoneyUI : CountRenderer
{
    [SerializeField] private MoneyManager _moneyManager;

    private void Start()
    {
        _moneyManager.MoneyChanged += UpdateCount;
        UpdateCount(_moneyManager.MoneyAmount);
    }
}
