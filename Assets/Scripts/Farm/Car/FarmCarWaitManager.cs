using System;
using System.Linq;
using UnityEngine;

public class FarmCarWaitManager : MonoBehaviour, IUpgradeable
{
    [SerializeField] private FarmCar _car;
    [SerializeField] private KitchenStorage _kitchenStorage;
    [SerializeField] private float _waitTime;
    private IngredientCountList _ingredientsSended;
    public float NowTime { get; private set; }
    public bool IsWait { get; private set; }
    public bool IsSended { get; private set; }

    [Header("Upgrades")]
    [SerializeField] private CountUpgrade[] _speedUpgrades;

    public event Action<float> WaitStarted;

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_speedUpgrades.Contains(upgrade)) {
            var countUpgrade = upgrade as CountUpgrade;
            _waitTime = countUpgrade.Count;
            if (IsWait || IsSended) {
                NowTime = Mathf.Min(NowTime, _waitTime);
            }
        }
    }

    public void StartWait()
    {
        _ingredientsSended = _car.GetList();
        NowTime = _waitTime;
        IsWait = true;
        _car.Leave();
        WaitStarted?.Invoke(_waitTime);
    }

    private void Update()
    {
        if (!IsWait)
            return;

        if (NowTime > 0) {
            NowTime -= Time.deltaTime * TimeManager.instance.TimeSpeed;
        } else {
            if (IsSended) {
                IsWait = true;
                _car.Return();
            } else {
                _kitchenStorage.PutIngredients(_ingredientsSended);
                NowTime = _waitTime;
                IsSended = true;
            }
        }
    }
}
