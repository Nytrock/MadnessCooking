using System;
using UnityEngine;

public class FarmBed : MonoBehaviour
{
    private FarmBedUIManager _UI;
    private BedTypeHolder _bedHolder;
    private Ingredient _plantedIngredient;
    private FarmCar _car;
    private WheatManager _wheatManager;

    private float _growTime;
    private float _nowTime;
    private int _count;
    private bool _isFull;
    private bool _isActive;

    private float _waterBoost = 1;
    private float _fertilizeBoost = 1;
    private float _pestsSlowdown = 1;

    public BedType BedType => _bedHolder.Type;
    public Ingredient Ingredient => _plantedIngredient;
    public int Count => _count;
    public bool IsActive => _isActive;

    public event Action CountChanged;

    public void MouseDown()
    {
        if (_plantedIngredient == null)
            _UI.ActivateIngredientChoice(this);
        else
            _UI.ShowGroundBed(this);
    }

    private void Update()
    {
        if (_isFull || _plantedIngredient == null)
            return;

        if (_nowTime < _growTime) {
            _nowTime += Time.deltaTime * _waterBoost * _fertilizeBoost 
                * _pestsSlowdown * TimeManager.instance.TimeSpeed;
        } else {
            _count++;
            CountChanged?.Invoke();
            _nowTime = 0;
            if (_count == _plantedIngredient.MaxCount)
                _isFull = true;
        }
    }

    public void ResetIngredient()
    {
        _plantedIngredient = null;
        _count = 0;
        _isFull = false;
        _bedHolder.StopAnimation();
        _UI.ForgiveBed(this);
    }

    public void SetBedType(BedTypeHolder bedType)
    {
        if (bedType.Type.AcceptableType == IngredientType.Water)
            _waterBoost = 0;
        else
            _waterBoost = 1;

        _bedHolder = bedType;
        _isActive = true;
        _bedHolder.ChangeMode(true);
    }

    public void ResetBedType()
    {
        ResetIngredient();
        _bedHolder.ChangeMode(false);
        _bedHolder = null;
        _isActive = false;
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
        _nowTime = 0;
    }

    public void SendIngredients()
    {
        if (_count == 0)
            return;

        if (_plantedIngredient.name == "Wheat") {
            _wheatManager.AddWheat(_count);
            _count = 0;
            UpdateCount();
            return;
        }

        var carSpace = _car.GetSpace();
        if (carSpace == 0) {
            return;
        }

        if (carSpace < _count) {
            _car.PutIngredient(new IngredientCount(_plantedIngredient, carSpace));
            _count -= carSpace;
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

    public void Water()
    {
        _waterBoost = _bedHolder.GetWaterMultiplier();
    }

    public void Fertilize()
    {
        _fertilizeBoost = _bedHolder.GetFertilizeMultiptier();
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

    public PestsGenerator GetPests()
    {
        return _bedHolder.GetPests();
    }
}
