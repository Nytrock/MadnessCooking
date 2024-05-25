using System;
using UnityEngine;

[RequireComponent(typeof(FarmBedUpgrader))]
public class FarmBed : MonoBehaviour
{
    [Header("Upgrades")]
    [SerializeField] private FarmBedGrowSlider _growStatusSlider;

    public FarmBedData BedData { get; private set; }
    private FarmData _data;

    private WheatManager _wheatManager;
    private Ingredient _wheat;

    private BedTypeHolder _bedHolder;
    private FarmBedUpgrader _upgrader;
    private FarmBedUIManager _UI;
    private FarmCar _car;
    private float _growTime;

    public PestsGenerator PestsGenerator => _bedHolder.PestsGenerator;

    public event Action CountChanged;

    private void Awake()
    {
        _upgrader = GetComponent<FarmBedUpgrader>();
    }

    public void MouseDown()
    {
        if (BedData.PlantedIngredient == null)
            _UI.ActivateIngredientChoice(this);
        else
            _UI.ChangeState(this);
    }

    private void LateStart()
    {
        UpdateUpgrades();
    }

    private void Update()
    {
        if (BedData.IsFull || BedData.PlantedIngredient == null)
            return;

        if (BedData.NowTime < _growTime) {
            BedData.NowTime += BedData.SummarizedBoost * TimeManager.Instance.InGameTimeSpeed;
        } else {
            BedData.Count++;
            BedData.NowTime = 0;
            if (_data.IsAutoWheat && BedData.PlantedIngredient == _wheat) {
                _wheatManager.AddWheat(BedData.Count);
                BedData.Count = 0;
                return;
            }

            CountChanged?.Invoke();
            if (BedData.Count == BedData.PlantedIngredient.MaxCount)
                BedData.IsFull = true;
            _bedHolder.UpdateAnimation();
        }

        if (_data.IsGrowStatusShow)
            _growStatusSlider.UpdateSlider(BedData.NowTime);
    }

    public void ResetIngredient()
    {
        BedData.PlantedIngredient = null;
        BedData.Count = 0;
        BedData.NowTime = 0;
        BedData.IsFull = false;
        _bedHolder.StopAnimation();
        UpdateUpgrades();
    }

    public void SetBedType(BedTypeHolder bedType)
    {
        _bedHolder = bedType;
        _bedHolder.ChangeMode(true);

        BedData.BedType = _bedHolder.Type;
        if (BedData.BedType.AcceptableType == IngredientType.Water)
            BedData.WaterBoost.Boost = 0;
        else
            BedData.WaterBoost.Boost = 1;
    }

    public void ResetBedType()
    {
        if (BedData.BedType.Cost > 0)
            MoneyManager.Instance.ChangeMoney(BedData.BedType.Cost);
        BedData.IsActive = false;

        ResetIngredient();
        _bedHolder.ChangeMode(false);
        DisableUpgrades();

        _bedHolder = null;
        BedData.BedType = null;
    }

    public void Setup(FarmBedSettings settings)
    {
        _UI = settings.UIManager;
        _car = settings.Car;
        _wheatManager = settings.WheatManager;
        _wheat = IngredientsManager.Instance.Wheat;
    }

    public void SetIngredient(Ingredient ingredient)
    {
        BedData.PlantedIngredient = ingredient;
        _bedHolder.SetIngredient();
        _growTime = ingredient.TimeGrow;
        _growStatusSlider.SetMaxTime(_growTime);
        UpdateUpgrades();
    }

    public void SendIngredients()
    {
        if (BedData.Count == 0)
            return;

        if (BedData.PlantedIngredient == _wheat) {
            _wheatManager.AddWheat(BedData.Count);
            BedData.Count = 0;
            UnfullBed();
            return;
        }

        if (_car.Data.LeftSpace == 0)
            return;

        int remainCount = _car.PutIngredientWithRemain(new IngredientCount(BedData.PlantedIngredient, BedData.Count));
        FatigueManager.Instance.ChangeFatigue(BedData.PlantedIngredient.FatigueCount 
            * (BedData.Count - remainCount));
        BedData.Count = remainCount; 

        UnfullBed();
    }

    private void UnfullBed()
    {
        CountChanged?.Invoke();
        if (BedData.IsFull) {
            BedData.IsFull = false;
            _bedHolder.UpdateAnimation();
        }
    }

    public void ChangeEternalWater()
    {
        _bedHolder.ChangeEternalWater();
        _UI.UpdateSideButtons();
    }

    public void Water()
    {
        _bedHolder.Water();
    }

    public void ChangeEternalFertilize()
    {
        _bedHolder.ChangeEternalFertilize();
        _UI.UpdateSideButtons();
    }

    public void Fertilize()
    {
        _bedHolder.Fertilize();
    }

    private void UpdateUpgrades()
    {
        _growStatusSlider.SetActive(_data.IsGrowStatusShow && BedData.PlantedIngredient != null);
    }

    public bool HaveUpgrade(FarmBedUpgrade upgrade) => _upgrader.HaveUpgrade(upgrade);

    private void DisableUpgrades()
    {
        _upgrader.DisableUpgrades();
        ChangeEternalWater();
        ChangeEternalFertilize();
    }

    public void AddUpgrade(FarmBedUpgrade upgrade)
    {
        _upgrader.AddUpgrade(upgrade);
        ChangeEternalWater();
        ChangeEternalFertilize();

        if (BedData.PestsGenerator.IsPestsRemoved)
            RemovePests();
    }

    private void RemovePests()
    {
        _UI.UpdateSideButtons();
        PestsGenerator.CleanPests();
    }

    public void Bind(FarmData data, FarmBedData bedData, BedTypeHolder holder)
    {
        _data = data;
        BedData = bedData;

        if (BedData.BedType != null) {
            SetBedType(holder);
            if (BedData.PlantedIngredient != null)
                SetIngredient(BedData.PlantedIngredient);
        }

        _upgrader.Bind(BedData);
        LateStart();
    }
}
