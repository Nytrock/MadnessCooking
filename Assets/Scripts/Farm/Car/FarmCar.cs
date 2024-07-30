using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FarmCar : SaveableIngredientStorage<FarmData> {
    [SerializeField] private UpgradeManager _upgradeManager;

    [Header("Upgrades")]
    [SerializeField] private CountUpgrade[] _sizeUpgrades;

    private Animator _animator;

    private void Awake() {
        _upgradeManager.ItemAdded += CheckSizeChanged;
        _animator = GetComponent<Animator>();
    }

    public void CheckSizeChanged(BaseUpgrade upgrade) {
        if (_sizeUpgrades.Contains(upgrade))
            Data.UpdateMaxSpace(upgrade as CountUpgrade);
    }

    public void Leave() {
        Data.ClearList();
        _animator.SetBool("isLeave", true);
    }

    public void InstantLeave() {
        _animator.SetBool("isLeave", true);
        _animator.Play(nameof(CarState.Sent), -1, 1);
    }

    public void Return() {
        _animator.SetBool("isLeave", false);
    }

    public override void Bind(FarmData data) {
        data.Car ??= new(_defaultMaxSpace);
        Data = data.Car;
        base.Bind(data);
    }
}
