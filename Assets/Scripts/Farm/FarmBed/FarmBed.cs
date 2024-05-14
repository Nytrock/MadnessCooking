using System;
using UnityEngine;

[RequireComponent(typeof(FarmBedUpgrader))]
public class FarmBed : MonoBehaviour
{
    [SerializeField] private Ingredient _wheat;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _autoWheatUpgrade;
    [SerializeField] private BaseUpgrade _growStatusShowUpgrade;
    [SerializeField] private FarmBedGrowSlider _growStatusSlider;
    private bool _isAutoWheat;
    private bool _isGrowStatusShow;

    private FarmBedUIManager _UI;
    private BedTypeHolder _bedHolder;
    private Ingredient _plantedIngredient;
    private FarmCar _car;
    private WheatManager _wheatManager;

    private float _growTime;
    private float _nowTime;
    private int _count;
    private bool _isFull;

    private float _waterBoost = 1;
    private float _fertilizeBoost = 1;
    private float _independentBoost = 1;
    private float _pestsSlowdown = 1;

    public BedType BedType => _bedHolder.Type;
    public PestsGenerator PestsGenerator => _bedHolder.PestsGenerator;
    public Ingredient Ingredient => _plantedIngredient;
    public int Count => _count;

    public bool IsActive { get; private set; }
    public FarmBedUpgrader Upgrader { get; private set; }


    public event Action CountChanged;

    private void Awake()
    {
        Upgrader = GetComponent<FarmBedUpgrader>();
    }

    public void MouseDown()
    {
        if (_plantedIngredient == null)
            _UI.ActivateIngredientChoice(this);
        else
            _UI.ShowGroundBed(this);
    }

    private void Start()
    {
        UpdateUpgrades();
    }

    private void Update()
    {
        if (_isFull || _plantedIngredient == null)
            return;

        if (_nowTime < _growTime) {
            _nowTime += Time.deltaTime * _waterBoost * _fertilizeBoost 
                * _pestsSlowdown * _independentBoost * TimeManager.instance.TimeSpeed;
        } else {
            _nowTime = 0;
            if (_isAutoWheat && _plantedIngredient == _wheat) {
                _wheatManager.AddWheat(1);
                return;
            }
            _count++;
            CountChanged?.Invoke();
            if (_count == _plantedIngredient.MaxCount)
                _isFull = true;
        }

        if (_isGrowStatusShow)
            _growStatusSlider.UpdateSlider(_nowTime);
    }

    public void ResetIngredient()
    {
        _plantedIngredient = null;
        _count = 0;
        _isFull = false;
        _bedHolder.StopAnimation();
        UpdateUpgrades();
    }

    public void SetBedType(BedTypeHolder bedType)
    {
        if (bedType.Type.AcceptableType == IngredientType.Water)
            _waterBoost = 0;
        else
            _waterBoost = 1;

        _bedHolder = bedType;
        Upgrader.UpdateBedHolder(_bedHolder);
        IsActive = true;
        _bedHolder.ChangeMode(true);
    }

    public void ResetBedType()
    {
        ResetIngredient();
        _bedHolder.ChangeMode(false);
        Upgrader.UpdateBedHolder(_bedHolder);
        Upgrader.ReturnUpgrades();

        _bedHolder = null;
        IsActive = false;
    }

    public void Setup(FarmBedSettings settings)
    {
        _UI = settings.UIManager;
        _car = settings.Car;
        _wheatManager = settings.WheatManager;
    }

    public void SetIngredient(Ingredient ingredient)
    {
        _plantedIngredient = ingredient;
        _bedHolder.SetIngredient(ingredient);
        _growTime = ingredient.TimeGrow;
        _growStatusSlider.SetMaxTime(_growTime);
        _nowTime = 0;
        UpdateUpgrades();
    }

    public void SendIngredients()
    {
        if (_count == 0)
            return;

        FatigueManager.instance.ChangeFatigue(_plantedIngredient.FatigueCount * _count);
        if (_plantedIngredient == _wheat) {
            _wheatManager.AddWheat(_count);
            _count = 0;
            UpdateCount();
            return;
        }

        if (_car.LeftSpace == 0)
            return;

        if (_car.LeftSpace < _count) {
            _car.PutIngredient(new IngredientCount(_plantedIngredient, _car.LeftSpace));
            _count -= _car.LeftSpace;
        } else {
            _car.PutIngredient(new IngredientCount(_plantedIngredient, _count));
            _count = 0;
        }

        UpdateCount();
    }

    private void UpdateCount()
    {
        CountChanged?.Invoke();
        _bedHolder.ResetAnimation(_isFull);
        _isFull = false;
    }

    public void EternalWater()
    {
        Water();
        _UI.UpdateSideButtons();
    }

    public void Water()
    {
        _waterBoost = _bedHolder.GetWaterMultiplier();
        ChangeAnimationSpeed();
    }

    public void EternalFertilize()
    {
        Fertilize();
        _UI.UpdateSideButtons();
    }


    public void Fertilize()
    {
        _fertilizeBoost = _bedHolder.GetFertilizeMultiptier();
        ChangeAnimationSpeed();
    }

    public void StopWaterBuff(float newMultiplier)
    {
        _waterBoost = newMultiplier;
        ChangeAnimationSpeed();
    }

    public void StopFertilizeBuff(float newMultiplier)
    {
        _fertilizeBoost = newMultiplier;
        ChangeAnimationSpeed();
    }

    public void ChangePestSlowdown(float coef)
    {
        _pestsSlowdown = coef;
        ChangeAnimationSpeed();
    }

    private void ChangeAnimationSpeed()
    {
        _bedHolder.BoostAnimationSpeed(_fertilizeBoost * _waterBoost * _pestsSlowdown);
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _growStatusShowUpgrade) {
            _isGrowStatusShow = true;
        } else if (upgrade == _autoWheatUpgrade) {
            _isAutoWheat = true;
            if (_plantedIngredient == _wheat)
                SendIngredients();
        }

        UpdateUpgrades();
    }

    private void UpdateUpgrades()
    {
        _growStatusSlider.SetActive(_isGrowStatusShow && _plantedIngredient != null);
    }

    public void UpdateUpgradeBooster()
    {
        _independentBoost = Upgrader.UpgradesBooster;
    }

    public void RemovePests()
    {
        _UI.UpdateSideButtons();
        PestsGenerator.CleanPests();
        PestsGenerator.SetRemoved(this);
    }
}
