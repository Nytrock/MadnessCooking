using System;
using UnityEngine;

[RequireComponent(typeof(FarmBedUpgrader))]
public class FarmBed : MonoBehaviour
{
    [SerializeField] private Ingredient _wheat;

    [Header("Upgrades")]
    [SerializeField] private FarmBedGrowSlider _growStatusSlider;

    public SerializableFarmBed BedData { get; private set; }
    private FarmData _data;

    private WheatManager _wheatManager;
    private BedTypeHolder _bedHolder;
    private FarmBedUIManager _UI;
    private FarmCar _car;
    private float _growTime;

    public PestsGenerator PestsGenerator => _bedHolder.PestsGenerator;
    public FarmBedUpgrader Upgrader { get; private set; }

    public event Action CountChanged;

    private void Awake()
    {
        Upgrader = GetComponent<FarmBedUpgrader>();
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
            BedData.NowTime += Time.deltaTime * BedData.SummarizedBoost 
                * TimeManager.instance.TimeSpeed;
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
        ResetIngredient();
        _bedHolder.ChangeMode(false);
        Upgrader.ReturnUpgrades();

        _bedHolder = null;
        BedData.BedType = null;
    }

    public void Setup(FarmBedSettings settings)
    {
        _UI = settings.UIManager;
        _car = settings.Car;
        _wheatManager = settings.WheatManager;
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

        int sendingCount = BedData.Count;
        if (_car.Data.LeftSpace < BedData.Count)
            sendingCount = _car.Data.LeftSpace;

        FatigueManager.instance.ChangeFatigue(BedData.PlantedIngredient.FatigueCount * sendingCount);
        BedData.Count -= sendingCount;
        _car.PutIngredient(new IngredientCount(BedData.PlantedIngredient, sendingCount));

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

    public void RemovePests()
    {
        _UI.UpdateSideButtons();
        PestsGenerator.CleanPests();
    }

    public void Bind(FarmData data, SerializableFarmBed bedData)
    {
        _data = data;
        BedData = bedData;

        if (BedData.BedType != null) {
            GetComponent<BedChoice>().SetType(BedData.BedType);
            if (BedData.PlantedIngredient != null)
                SetIngredient(BedData.PlantedIngredient);
        }

        LateStart();
    }
}
