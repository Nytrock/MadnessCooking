using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FarmCar : SaveableIngredientStorage<FarmData> {
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private GameTimeManager _timeManager;

    [Header("Upgrades")]
    [SerializeField] private CountUpgrade[] _sizeUpgrades;

    private Animator _animator;

    public event Action<CarState> StateChanged;

    private void Awake() {
        _timeManager.TimeSpeedUpdated += UpdateAnimationSpeed;
        _upgradeManager.ItemAdded += CheckSizeChanged;
        _animator = GetComponent<Animator>();
    }

    private void UpdateAnimationSpeed() {
        _animator.SetFloat("animationSpeed", InGameTime.Instance.NormalizedTime);
    }

    public void CheckSizeChanged(BaseUpgrade upgrade) {
        if (_sizeUpgrades.Contains(upgrade))
            Data.UpdateMaxSpace(upgrade as CountUpgrade);
    }

    public void Leave(IEnumerable<IngredientCount> ingredientsToDelete) {
        RemoveIngredients(ingredientsToDelete);
        _animator.SetBool("isLeave", true);
        StateChanged?.Invoke(CarState.Sent);
    }

    public void InstantLeave() {
        _animator.SetBool("isLeave", true);
        _animator.Play(nameof(CarState.Sent), -1, 1);
    }

    public void Return() {
        _animator.SetBool("isLeave", false);
        StateChanged?.Invoke(CarState.Returns);
    }

    public override void Bind(FarmData data) {
        data.Car ??= new(_defaultIngredients);
        Data = data.Car;
    }
}
