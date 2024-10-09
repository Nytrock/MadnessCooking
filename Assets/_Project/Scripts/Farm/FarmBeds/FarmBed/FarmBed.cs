using System;
using UnityEngine;

[RequireComponent(typeof(FarmBedUpgrader))]
public class FarmBed : MonoBehaviour {
    [Header("Upgrades")]
    [SerializeField] private FarmBedGrowSlider _growStatusSlider;

    public FarmBedData Data { get; private set; }
    private FarmUpgradeData _upgradeData;

    private WheatManager _wheatManager;
    private Ingredient _wheat;
    private FarmCar _car;
    private Puncher _puncher;

    private BedTypeHolder _bedHolder;
    private FarmBedUpgrader _upgrader;
    private FarmBedUIManager _UI;
    private float _growTime;

    public PestsGenerator PestsGenerator => _bedHolder.PestsGenerator;

    public event Action CountChanged;
    public event Action BedReseted;

    private void Awake() {
        _upgrader = GetComponent<FarmBedUpgrader>();
    }

    public void MouseDown() {
        if (Data.PlantedIngredient == null)
            _UI.ActivateIngredientChoice(this);
        else
            _UI.ChangeState(this);
    }

    private void LateStart() {
        UpdateUpgrades();
    }

    private void Update() {
        if (Data.IsFull || Data.PlantedIngredient == null)
            return;

        Data.UpdateTime();
        if (Data.NowTime > _growTime) {
            Data.AddIngredient();
            if (Data.IsAutoCollect) {
                SendIngredients();
                return;
            }

            CountChanged?.Invoke();
            _bedHolder.UpdateAnimation();
        }

        if (_upgradeData.IsGrowStatusShow)
            _growStatusSlider.SetValue(Data.NowTime);
    }

    public void ResetIngredient() {
        Data.ResetIngredient();
        _bedHolder.StopAnimation();
        UpdateUpgrades();
    }

    public void SetBedType(BedTypeHolder bedType) {
        _bedHolder = bedType;
        _bedHolder.ChangeMode(true);
        Data.SetBedType(_bedHolder.Type);

        if (bedType.Type.AcceptableType == IngredientType.Ghost)
            SetIngredient(ConstIngredients.Instance.Ectoplasm);
    }

    public void ResetBedType() {
        if (Data.BedType.Price > 0)
            MoneyManager.Instance.ChangeMoney(Data.BedType.Price);

        ResetIngredient();
        _bedHolder.ChangeMode(false);
        DisableUpgrades();

        _bedHolder = null;
        BedReseted?.Invoke();
        Data.ResetBedType();
    }

    public void Setup(FarmBedSettings settings) {
        _UI = settings.UIManager;
        _car = settings.Car;
        _puncher = settings.Puncher;
        _wheatManager = settings.WheatManager;
        _wheat = ConstIngredients.Instance.Wheat;
    }

    public void SetIngredient(Ingredient ingredient) {
        Data.SetIngredient(ingredient);
        _bedHolder.SetIngredient();

        _growTime = ingredient.TimeGrow;
        _growStatusSlider.SetMaxValue(_growTime);
        _growStatusSlider.SetValue(0);

        UpdateUpgrades();
    }

    public void SendIngredients() {
        if (Data.Count == 0)
            return;

        if (Data.PlantedIngredient == _wheat) {
            _wheatManager.AddWheat(Data.Count);
            Data.SetCount(0);
            UnfullBed();
            return;
        }

        if (_car.Data.LeftSpace == 0)
            return;

        int remainCount = _car.PutIngredientWithRemain(Data.PlantedIngredient, Data.Count);
        FatigueManager.Instance.ChangeFatigue(Data.PlantedIngredient.FatigueCoef
            * (Data.Count - remainCount));
        Data.SetCount(remainCount);
        _puncher.AddWaste(remainCount * Data.PlantedIngredient.WasteAmount);

        UnfullBed();
    }

    private void UnfullBed() {
        CountChanged?.Invoke();
        if (Data.IsFull) {
            Data.Unfull();
            _bedHolder.UpdateAnimation();
        }
    }

    public void ChangeEternalWater() {
        _bedHolder.ChangeEternalWater();
        _UI.UpdateSideButtons();
    }

    public void Water() {
        _bedHolder.Water();
    }

    public void ChangeEternalFertilize() {
        _bedHolder.ChangeEternalFertilize();
        _UI.UpdateSideButtons();
    }

    public void Fertilize() {
        _bedHolder.Fertilize();
    }

    private void UpdateUpgrades() {
        _growStatusSlider.ChangeState(_upgradeData.IsGrowStatusShow && Data.PlantedIngredient != null);
    }

    public bool HaveUpgrade(FarmBedUpgrade upgrade) => _upgrader.HaveUpgrade(upgrade);

    private void DisableUpgrades() {
        _upgrader.DisableUpgrades();
        ChangeEternalWater();
        ChangeEternalFertilize();
    }

    public void AddUpgrade(FarmBedUpgrade upgrade) {
        _upgrader.AddUpgrade(upgrade);
        ChangeEternalWater();
        ChangeEternalFertilize();

        if (Data.PestsGenerator.IsPestsRemoved)
            RemovePests();
    }

    private void RemovePests() {
        _UI.UpdateSideButtons();
        PestsGenerator.CleanPests();
    }

    public void Bind(FarmData data, FarmBedData bedData, BedTypeHolder holder) {
        _upgradeData = data.UpgradeData;
        Data = bedData;

        if (Data.BedType != null) {
            SetBedType(holder);
            if (Data.PlantedIngredient != null)
                SetIngredient(Data.PlantedIngredient);
        }

        _upgrader.Bind(Data);
        LateStart();
    }
}
