using System;
using System.Linq;
using UnityEngine;

public class FarmCarWaitManager : MonoBehaviour, IBindable<FarmData> {
    [SerializeField] private FarmCar _car;
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private KitchenStorage _kitchenStorage;
    [SerializeField, Min(0)] private float _defaultWaitTime;
    private CarWaitManagerData _data;

    [Header("Upgrades")]
    [SerializeField] private CountUpgrade[] _speedUpgrades;

    public float NowWaitTime => _data.NowWaitTime;

    public event Action<CarState> StateChanged;

    private void Awake() {
        _upgradeManager.ItemAdded += CheckSpeedChanged;
    }

    private void LateStart() {
        StateChanged?.Invoke(_data.CarState);
    }

    public void CheckSpeedChanged(BaseUpgrade upgrade) {
        if (_speedUpgrades.Contains(upgrade)) {
            var countUpgrade = upgrade as CountUpgrade;
            _data.UpdateSpeed(countUpgrade);
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
        data.CarWaitManager ??= new(_defaultWaitTime);
        _data = data.CarWaitManager;

        if (_data.CarState != CarState.Calm)
            _car.InstantLeave();
        LateStart();
    }
}
