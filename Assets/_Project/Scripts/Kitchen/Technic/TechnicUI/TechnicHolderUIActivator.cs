using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    public class TechnicHolderUIActivator : UIActivator {
        [SerializeField] private TechnicHolder _technicHolder;
        private TechnicRepairUI _repairUI;

        protected void Awake() {
            _repairUI = _activableObject.Value as TechnicRepairUI;
        }

        protected override void Press() {
            if (_repairUI == null)
                return;

            if (_technicHolder.Data.IsCooking || _technicHolder.Data.IsRepairing)
                return;

            _repairUI.SetTechnic(_technicHolder);
            base.Press();
        }
    }
}
