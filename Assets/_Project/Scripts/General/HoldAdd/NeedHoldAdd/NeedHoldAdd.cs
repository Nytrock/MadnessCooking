using System;
using UnityEngine;

namespace MadnessCooking.General {
    public abstract class NeedHoldAdd : HoldAdd {
        [SerializeField, Min(0)] protected int _materialDefaultCount;

        public NeedHoldAddData NeedHoldData => Data as NeedHoldAddData;

        public event Action MaterialCountChanged;

        public override void LateStart() {
            if (NeedHoldData == null)
                throw new ArgumentNullException("Argument for data or for UI are null");
            base.LateStart();

            MaterialCountChanged?.Invoke();
        }

        protected override void UpdateTimer() {
            if (NeedHoldData.MaterialCount == 0)
                return;

            base.UpdateTimer();
        }

        public override void ChangeClickMode(bool newValue) {
            if (NeedHoldData.MaterialCount == 0) {
                InvokeClickChanged(newValue);
                Data.ChangeWork(newValue);
                return;
            }

            base.ChangeClickMode(newValue);
        }

        protected override void AddReady() {
            SubstractMaterial();
            base.AddReady();

            if (NeedHoldData.MaterialCount == 0)
                InvokeWorkChanged(false);
        }

        public void SubstractMaterial() {
            NeedHoldData.SubstractMaterial();
            MaterialCountChanged?.Invoke();
        }

        public void AddMaterial(int count) {
            NeedHoldData.AddMaterial(count);
            MaterialCountChanged?.Invoke();
        }

        public void SetMaterial(int count) {
            NeedHoldData.SetMaterial(count);
            MaterialCountChanged?.Invoke();
        }

        public void ClearMaterials() {
            NeedHoldData.SetMaterial(0);
            MaterialCountChanged?.Invoke();
        }
    }
}
