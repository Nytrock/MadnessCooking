using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    [RequireComponent(typeof(Animator))]
    public class FlourMill : NeedHoldAdd {
        [SerializeField] private IngredientManager _ingredientsManager;
        [SerializeField] private ParticleSystem _flourParticle;
        [SerializeField] private Puncher _puncher;
        [SerializeField, Min(0)] private float _wasteAmount;

        private Animator _animator;

        protected override void Awake() {
            base.Awake();
            _animator = GetComponent<Animator>();
            WorkChanged += ChangeAnimationState;
        }

        protected override void AddReady() {
            _puncher.AddWaste(_wasteAmount);
            base.AddReady();
        }

        public override void ChangeClickMode(bool newValue) {
            base.ChangeClickMode(newValue);
        }

        public override void Bind(FarmData data) {
            data.FlourMill ??= new(_readyDefaultCount, _materialDefaultCount);
            Data = data.FlourMill;
        }

        protected override void UpdateUpgrades() {
            base.UpdateUpgrades();
            if (Data.IsUnlocked)
                _ingredientsManager.AddItem(ConstIngredients.Instance.Flour);
        }

        private void ChangeAnimationState(bool newState) {
            _animator.SetBool("isHold", newState);
            _flourParticle.ChangeState(newState);
        }
    }
}
