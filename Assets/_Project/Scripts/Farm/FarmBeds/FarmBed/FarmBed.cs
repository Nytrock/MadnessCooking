using System;
using UnityEngine;

[RequireComponent(typeof(FarmBedUpgrader))]
public class FarmBed : MonoBehaviour {
    [SerializeField] private GameObject _autoCollector;

    [Header("Upgrades")]
    [SerializeField] private FarmBedGrowSlider _growStatusSlider;

    public FarmBedData Data { get; private set; }
    private FarmUpgradeData _upgradeData;
    private FarmBedManagerData _managerData;

    private WheatManager _wheatManager;
    private FarmBedUIManager _UI;
    private FarmCar _car;
    private Puncher _puncher;

    private BedTypeHolder _bedHolder;
    private FarmBedUpgrader _upgrader;

    public PestsGenerator PestsGenerator => _bedHolder.PestsGenerator;

    public event Action CountChanged;
    public event Action BedWatered;
    public event Action BedFertilized;
    public event Action BedReseted;

    private void Awake() {
        _upgrader = GetComponent<FarmBedUpgrader>();
    }

    public void MouseDown() {
        if (Data.PlantedIngredient == null)
            _UI.ActivateIngredientChoice(this);
        else
            _UI.SetFarmBed(this);
    }

    private void LateStart() {
        UpdateGlobalUpgrades();
    }

    private void Update() {
        if (Data.IsFull || Data.PlantedIngredient == null)
            return;

        Data.UpdateTime();
        if (Data.NowTime > Data.PlantedIngredient.TimeGrow) {
            Data.AddIngredient();
            if (Data.IsAutoCollect) {
                CollectIngredients();
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
        PestsGenerator.ChangeState(false);
        _bedHolder.StopAnimation();
        UpdateGlobalUpgrades();
    }

    public void SetBedType(BedTypeHolder bedType) {
        _bedHolder = bedType;
        _bedHolder.ChangeState(true);
        Data.SetBedType(_bedHolder.Type);

        if (bedType.Type.AcceptableType == IngredientType.Ghost)
            SetIngredient(ConstIngredients.Instance.Ectoplasm);
    }

    public void ResetBedType() {
        if (Data.BedType.Price > 0)
            MoneyManager.Instance.ChangeMoney(Data.BedType.Price);

        ResetIngredient();
        _bedHolder.ChangeState(false);
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
    }

    public void SetIngredient(Ingredient ingredient) {
        Data.SetIngredient(ingredient);
        _bedHolder.SetIngredient();

        _growStatusSlider.SetMaxValue(ingredient.TimeGrow);
        _growStatusSlider.SetValue(0);

        UpdateGlobalUpgrades();
    }

    public void CollectIngredients() {
        if (Data.Count == 0)
            return;

        if (Data.PlantedIngredient == ConstIngredients.Instance.Wheat) {
            _wheatManager.AddWheat(Data.Count);
            _managerData.AddToPlantCount(Data.Count);
            Data.SetCount(0);
            UnfullBed();
            return;
        }

        if (_car.Data.LeftSpace == 0)
            return;

        int remainCount = _car.PutIngredientWithRemain(Data.PlantedIngredient, Data.Count);
        int puttedCount = Data.Count - remainCount;

        _managerData.AddToPlantCount(puttedCount);
        FatigueManager.Instance.AddFatigue(Data.PlantedIngredient.FatigueCoef * puttedCount);
        _puncher.AddWaste(puttedCount * Data.PlantedIngredient.WasteAmount);
        Data.SetCount(remainCount);

        UnfullBed();
    }

    private void UnfullBed() {
        CountChanged?.Invoke();
        if (!Data.IsFull)
            return;

        Data.Unfull();
        _bedHolder.UpdateAnimation();
    }

    public void Water() {
        _bedHolder.Water();
        BedWatered?.Invoke();
    }

    public void Fertilize() {
        _bedHolder.Fertilize();
        BedFertilized?.Invoke();
    }

    public void UpdateGlobalUpgrades() {
        _growStatusSlider.ChangeState(_upgradeData.IsGrowStatusShow && Data.PlantedIngredient != null);
    }

    public bool HaveUpgrade(FarmBedUpgrade upgrade) => _upgrader.HaveUpgrade(upgrade);

    private void DisableUpgrades() {
        _upgrader.DisableUpgrades();
        _bedHolder.UpdateUpgrades();

        _autoCollector.SetActive(false);
    }

    public void AddUpgrade(FarmBedUpgrade upgrade) {
        _upgrader.AddUpgrade(upgrade);
        _bedHolder.UpdateUpgrades();

        if (Data.IsAutoCollect)
            ActivateAutoCollect();

        if (Data.PestsGenerator.IsPestsRemoved)
            RemovePests();

        _UI.UpdateSideButtons();
    }

    private void ActivateAutoCollect() {
        _autoCollector.SetActive(true);
        CollectIngredients();
    }

    private void RemovePests() {
        PestsGenerator.RemovePests();
    }

    public void Bind(FarmData data, FarmBedData bedData, BedTypeHolder holder) {
        _upgradeData = data.UpgradeData;
        _managerData = data.FarmBedGroups;
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