using System;
using UnityEngine;

public class GroundBed : MonoBehaviour
{
    private GroundBedUIManager _UI;
    private BedType _bedType;
    private BedTypeHolder _bedHolder;
    private Ingredient _plantedIngredient;
    private Car _car;
    private WheatManager _wheatManager;

    private float _growTime;
    private float _nowTime;
    private int _count;
    private bool _isFull;

    private float _waterBoost = 1;
    private float _fertilizeBoost = 1;

    public BedType BedType => _bedType;
    public Ingredient Ingredient => _plantedIngredient;
    public int Count => _count;

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
            _nowTime += Time.deltaTime * _waterBoost * _fertilizeBoost;
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
        _bedType = _bedHolder.Type;
        _bedHolder.ChangeMode(true);
    }

    public void ResetBedType()
    {
        ResetIngredient();
        _bedHolder.ChangeMode(false);
        _bedHolder = null;
        _bedType = null;
    }

    public void Setup(GroundBedSettings settings)
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
            _car.PutIngredient(carSpace, _plantedIngredient);
            _count -= carSpace;
        } else {
            _car.PutIngredient(_count, _plantedIngredient);
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
        _bedHolder.BoostAnimationSpeed(newMultiplier);
    }

    public void StopFertilizeBuff(float newMultiplier)
    {
        _fertilizeBoost = newMultiplier;
        _bedHolder.BoostAnimationSpeed(newMultiplier);
    }
}
