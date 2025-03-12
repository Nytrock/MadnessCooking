using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmCarWaitManager : MonoBehaviour, IBindable<FarmData> {
    [SerializeField] private FarmCar _car;
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private KitchenStorage _kitchenStorage;
    [SerializeField] private LocationNotificationManager _notificationManager;
    [SerializeField, Min(0)] private float _defaultWaitHours;
    [SerializeField, Min(0)] private float _notificationsLifeTime;

    [Header("Upgrades")]
    [SerializeField] private CoefficientUpgrade[] _speedUpgrades;

    private FarmCarWaitManagerData _waitData;
    private const int SECONDS_IN_MINUTES = 60;

    public float NowWaitTime => _waitData.NowWaitTime;
    public CarState CarState => _waitData.CarState;
    public IEnumerable<IngredientCount> IngredientsSended => _waitData.IngredientsSended;

    public event Action<CarState> StateChanged;

    private void Awake() {
        _upgradeManager.ItemAdded += CheckSpeedChanged;
    }

    public void LateStart() {
        if (_waitData.CarState != CarState.Calm)
            _car.InstantLeave();
        StateChanged?.Invoke(_waitData.CarState);
    }

    private void Update() {
        if (_waitData.CarState == CarState.Calm)
            return;

        if (_waitData.NowWaitTime > 0) {
            _waitData.UpdateTime();
        } else {
            if (_waitData.CarState == CarState.Returns)
                Return();
            else
                StartReturn();
            StateChanged?.Invoke(_waitData.CarState);
        }
    }

    public void Send() {
        _waitData.SetIngredientsSended(_car.Data.Ingredients);
        _waitData.StartWait();
        _car.Leave(_waitData.IngredientsSended);
        StateChanged?.Invoke(_waitData.CarState);
    }

    private void StartReturn() {
        _kitchenStorage.PutIngredients(_waitData.IngredientsSended);
        _waitData.StartReturn();
        _notificationManager.CreateNotification(Location.Kitchen, _notificationsLifeTime);
    }

    private void Return() {
        _waitData.StartCalm();
        _car.Return();
        _notificationManager.CreateNotification(Location.Farm, _notificationsLifeTime);
    }

    public void CheckSpeedChanged(BaseUpgrade upgrade) {
        if (_speedUpgrades.Contains(upgrade)) {
            var coefficientUpgrade = upgrade as CoefficientUpgrade;
            _waitData.UpdateSpeed(coefficientUpgrade.Coefficient * SECONDS_IN_MINUTES);
        }
    }

    public void Bind(FarmData data) {
        data.CarWaitManager ??= new(_defaultWaitHours * SECONDS_IN_MINUTES);
        _waitData = data.CarWaitManager;
    }
}
