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

    public SerializableCarWaitManager Data { get; private set; }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_speedUpgrades.Contains(upgrade)) {
            var countUpgrade = upgrade as CountUpgrade;
            Data.WaitTime = countUpgrade.Count;
            if (Data.CarState != CarState.Calm) {
                Data.NowTime = Mathf.Min(Data.NowTime, Data.WaitTime);
            }
        }
    }

    public void StartWait()
    {
        Data.IngredientsSended.Clear();
        Data.IngredientsSended.Extend(_car.Data.Ingredients);

        Data.NowTime = Data.WaitTime;
        Data.CarState = CarState.Sent;
        _car.Leave();
    }

    private void Update()
    {
        if (Data.CarState == CarState.Calm)
            return;

        if (Data.NowTime > 0) {
            Data.NowTime -= Time.deltaTime * TimeManager.instance.TimeSpeed;
        } else {
            if (Data.CarState == CarState.Returns) {
                Data.CarState = CarState.Calm;
                _car.Return();
            } else {
                Data.CarState = CarState.Returns;
                _kitchenStorage.PutIngredients(Data.IngredientsSended);
                Data.IngredientsSended.Clear();
                Data.NowTime = Data.WaitTime;
            }
        }
    }

    public void Bind(FarmData data, bool isFileEmpty)
    {
        if (isFileEmpty) {
            data.CarWaitManager = new() {
                WaitTime = _defaultWaitTime
            };
        }
        Data = data.CarWaitManager;
        
        if (Data.CarState != CarState.Calm)
            _car.InstantLeave();
    }
}
