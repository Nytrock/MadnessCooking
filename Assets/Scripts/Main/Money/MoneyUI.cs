using UnityEngine;

public class MoneyUI : CountRenderer
{
    [SerializeField] private MoneyManager _moneyManager;

    private void Awake()
    {
        _moneyManager.MoneyChanged += UpdateCount;
    }

    private void Start()
    {
        UpdateCount(_moneyManager.MoneyAmount);
    }
}
