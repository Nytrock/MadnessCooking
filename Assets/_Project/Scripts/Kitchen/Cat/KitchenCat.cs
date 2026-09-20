using MadnessCooking.General;
using System;
using UnityEngine;

namespace MadnessCooking.Kitchen {
    [RequireComponent(typeof(Collider2D), typeof(Animator))]
    public class KitchenCat : DecorHolder, IBindable<KitchenData> {
        private static readonly int IsPetHash = Animator.StringToHash("isPet");

        [SerializeField, Min(0)] private float _needTime;
        [SerializeField, Min(0)] private float _cheerfullCoef;
        [SerializeField] private KitchenCatEyes _eyes;
        private KitchenCatData _data;

        public event Action Petted;

        private Animator _animator;

        private void Awake() {
            _animator = GetComponent<Animator>();
        }

        private void OnMouseDown() {
            if (_data.IsPetted || FatigueManager.Instance.IsTired)
                return;

            Pet();
        }

        private void Pet() {
            _animator.SetTrigger(IsPetHash);
            _eyes.ChangeState(false);
            FatigueManager.Instance.RemoveFatigue(_cheerfullCoef);
            _data.Pet();

            Petted?.Invoke();
        }

        private void Update() {
            if (_data.IsPetted)
                UpdateEyes();
            _data.Update();
        }

        public void Bind(KitchenData data) {
            data.Cat ??= new(_needTime);
            _data = data.Cat;
        }

        public void LateStart() {
            UpdateEyes();
        }

        private void UpdateEyes() {
            _eyes.UpdateScale(_data);
        }

        public void EnableEyes() {
            _eyes.ChangeState(true);
        }
    }
}
