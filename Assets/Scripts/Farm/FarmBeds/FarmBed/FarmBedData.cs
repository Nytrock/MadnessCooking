using System;
using UnityEngine;

[Serializable]
public class FarmBedData {
    [SerializeField] private BedType _bedType;
    [SerializeField] private Ingredient _plantedIngredient;
    [SerializeField] private bool _isActive;

    [SerializeField] private float _nowTime;
    [SerializeField] private int _count;
    [SerializeField] private bool _isFull;
    [SerializeField] private float _animationTime;

    [SerializeField] private BedHolderBoosterData _waterBoost;
    [SerializeField] private BedHolderBoosterData _fertilizeBoost;
    [SerializeField] private PestsGeneratorData _pestsGenerator;
    [SerializeField] private float _independentBoost;
    [SerializeField] private bool _isAutoCollect;

    public BedType BedType => _bedType;
    public Ingredient PlantedIngredient => _plantedIngredient;
    public bool IsActive => _isActive;
    public float NowTime => _nowTime;
    public int Count => _count;
    public bool IsFull => _isFull;
    public float AnimationTime => _animationTime;
    public BedHolderBoosterData WaterBoost => _waterBoost;
    public BedHolderBoosterData FertilizeBoost => _fertilizeBoost;
    public PestsGeneratorData PestsGenerator => _pestsGenerator;
    public float SummarizedBoost => _waterBoost.Boost * _fertilizeBoost.Boost
        * _independentBoost * _pestsGenerator.PestsSlowdown;
    public bool IsAutoCollect => _isAutoCollect;

    public FarmBedData() {
        _waterBoost = new();
        _fertilizeBoost = new();
        _pestsGenerator = new();

        _nowTime = 0;
        _count = 0;
        _independentBoost = 1;
    }

    public void DisableUpgrades() {
        _waterBoost.DisableUpgrades();
        _fertilizeBoost.DisableUpgrades();
        _pestsGenerator.DisableUpgrades();
    }

    public void SetActive(bool isActive) {
        _isActive = isActive;
    }

    public void UpdateTime() {
        _nowTime += SummarizedBoost * InGameTime.Instance.DeltaTime;
    }

    public void AddIngredient() {
        _count++;
        _nowTime = 0;
        if (_count == _plantedIngredient.MaxCount)
            _isFull = true;
    }

    public void SetCount(int count) {
        _count = count;
    }

    public void Unfull() {
        _isFull = false;
    }

    public void ResetIngredient() {
        _plantedIngredient = null;
        _count = 0;
        _nowTime = 0;
        _isFull = false;
    }

    public void SetIngredient(Ingredient ingredient) {
        _plantedIngredient = ingredient;
    }

    public void ResetBedType() {
        _isActive = false;
        _bedType = null;
    }

    public void SetBedType(BedType bedType) {
        _bedType = bedType;
    }

    public void SetAnimationTime(float animationTime) {
        _animationTime = animationTime;
    }

    public void SetIndependentBoost(CoefficientFarmBedUpgrade upgrade) {
        _independentBoost = upgrade.Coefficient;
    }

    public void SetAutoCollect() {
        _isAutoCollect = true;
    }
}