using MadnessCooking.General;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MadnessCooking.Farm {
    [RequireComponent(typeof(Animator))]
    public class FarmCar : IngredientStorage, ISaveable {
        private static readonly int AnimationSpeedHash = Animator.StringToHash("animationSpeed");
        private static readonly int IsLeaveHash = Animator.StringToHash("isLeave");

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

        public void LateStart() {
            Data.SetMaxSpace(_defaultMaxSpace);
            InvokeLoadingDataEnded();
            foreach (var ingredientCount in Data.Ingredients)
                InvokeIngredientAdded(ingredientCount);
        }

        private void UpdateAnimationSpeed() {
            _animator.SetFloat(AnimationSpeedHash, InGameTime.Instance.NormalizedTime);
        }

        public void CheckSizeChanged(BaseUpgrade upgrade) {
            if (_sizeUpgrades.Contains(upgrade))
                Data.UpdateMaxSpace(upgrade as CountUpgrade);
        }

        public void Leave(IEnumerable<IngredientCount> ingredientsToDelete) {
            RemoveIngredients(ingredientsToDelete);
            _animator.SetBool(IsLeaveHash, true);
            StateChanged?.Invoke(CarState.Sent);
        }

        public void InstantLeave() {
            _animator.SetBool(IsLeaveHash, true);
            _animator.Play(nameof(CarState.Sent), -1, 1);
        }

        public void Return() {
            _animator.SetBool(IsLeaveHash, false);
            StateChanged?.Invoke(CarState.Returns);
        }

        public void LoadSave(GameData data) {
            data.Farm.Car ??= new(_defaultIngredients);
            Data = data.Farm.Car;
        }
    }
}
