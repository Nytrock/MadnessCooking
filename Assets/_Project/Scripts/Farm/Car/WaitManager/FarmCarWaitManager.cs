using System;
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

    private CarWaitManagerData _data;
    private const int SECONDS_IN_MINUTES = 60;

    public float NowWaitTime => _data.NowWaitTime;

    public event Action<CarState> StateChanged;

    private void Awake() {
        _upgradeManager.ItemAdded += CheckSpeedChanged;
    }

    public void LateStart() {
        if (_data.CarState != CarState.Calm)
            _car.InstantLeave();
        StateChanged?.Invoke(_data.CarState);
    }

    private void Update() {
        if (_data.CarState == CarState.Calm)
            return;

        if (_data.NowWaitTime > 0) {
            _data.UpdateTime();
        } else {
            if (_data.CarState == CarState.Returns)
                Return();
            else
                StartReturn();
            StateChanged?.Invoke(_data.CarState);
        }
    }

    public void Send() {
        _data.SetIngredientsSended(_car.Data.Ingredients);
        _data.StartWait();
        _car.Leave(_data.IngredientsSended);
        StateChanged?.Invoke(_data.CarState);
    }

    private void StartReturn() {
        _kitchenStorage.PutIngredients(_data.IngredientsSended);
        _data.StartReturn();
        _notificationManager.CreateNotification(Location.Kitchen, _notificationsLifeTime);
    }

    private void Return() {
        _data.StartCalm();
        _car.Return();
        _notificationManager.CreateNotification(Location.Farm, _notificationsLifeTime);
    }

    public void CheckSpeedChanged(BaseUpgrade upgrade) {
        if (_speedUpgrades.Contains(upgrade)) {
            var coefficientUpgrade = upgrade as CoefficientUpgrade;
            _data.UpdateSpeed(coefficientUpgrade.Coefficient * SECONDS_IN_MINUTES);
        }
    }

    public void Bind(FarmData data) {
        data.CarWaitManager ??= new(_defaultWaitHours * SECONDS_IN_MINUTES);
        _data = data.CarWaitManager;
    }
}
