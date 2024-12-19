using System;
using System.Linq;
using UnityEngine;

public class FarmCarWaitManager : MonoBehaviour, IBindable<FarmData> {
    [SerializeField] private FarmCar _car;
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private KitchenStorage _kitchenStorage;
    [SerializeField, Min(0)] private float _defaultWaitMinutes;

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

    public void CheckSpeedChanged(BaseUpgrade upgrade) {
        if (_speedUpgrades.Contains(upgrade)) {
            var coefficientUpgrade = upgrade as CoefficientUpgrade;
            _data.UpdateSpeed(coefficientUpgrade.Coefficient * SECONDS_IN_MINUTES);
        }
    }

    public void StartWait() {
        _data.SetIngredientsSended(_car.Data.Ingredients);
        _data.StartWait();
        _car.Leave();
        StateChanged?.Invoke(_data.CarState);
    }

    private void Update() {
        if (_data.CarState == CarState.Calm)
            return;

        if (_data.NowWaitTime > 0) {
            _data.UpdateTime();
        } else {
            if (_data.CarState == CarState.Returns) {
                _data.StartCalm();
                _car.Return();
            } else {
                _kitchenStorage.PutIngredients(_data.IngredientsSended);
                _data.StartReturn();
            }
            StateChanged?.Invoke(_data.CarState);
        }
    }

    public void Bind(FarmData data) {
        data.CarWaitManager ??= new(_defaultWaitMinutes * SECONDS_IN_MINUTES);
        _data = data.CarWaitManager;
    }
}
