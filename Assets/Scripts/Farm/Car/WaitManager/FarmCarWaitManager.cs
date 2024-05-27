using System;
using System.Linq;
using UnityEngine;

public class FarmCarWaitManager : MonoBehaviour, IUpgradeable, IBindable<FarmData>
{
    [SerializeField] private FarmCar _car;
    [SerializeField] private KitchenStorage _kitchenStorage;
    [SerializeField, Min(0)] private float _defaultWaitTime;

    [Header("Upgrades")]
    [SerializeField] private CountUpgrade[] _speedUpgrades;

    public CarWaitManagerData Data { get; private set; }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_speedUpgrades.Contains(upgrade)) {
            var countUpgrade = upgrade as CountUpgrade;
            Data.NeedWaitTime = countUpgrade.Count;
            if (Data.CarState != CarState.Calm) {
                Data.NowWaitTime = Mathf.Min(Data.NowWaitTime, Data.NeedWaitTime);
            }
        }
    }

    public void StartWait()
    {
        Data.IngredientsSended.Clear();
        Data.IngredientsSended.Extend(_car.Data.Ingredients);

        Data.NowWaitTime = Data.NeedWaitTime;
        Data.CarState = CarState.Sent;
        _car.Leave();
    }

    private void Update()
    {
        if (Data.CarState == CarState.Calm)
            return;

        if (Data.NowWaitTime > 0) {
            Data.NowWaitTime -= InGameTime.Instance.DeltaTime;
        } else {
            if (Data.CarState == CarState.Returns) {
                Data.CarState = CarState.Calm;
                _car.Return();
            } else {
                Data.CarState = CarState.Returns;
                _kitchenStorage.PutIngredients(Data.IngredientsSended);
                Data.IngredientsSended.Clear();
                Data.NowWaitTime = Data.NeedWaitTime;
            }
        }
    }

    public void Bind(FarmData data, bool isFileEmpty)
    {
        if (isFileEmpty) {
            data.CarWaitManager = new() {
                NeedWaitTime = _defaultWaitTime
            };
        }
        Data = data.CarWaitManager;
        
        if (Data.CarState != CarState.Calm)
            _car.InstantLeave();
    }
}
