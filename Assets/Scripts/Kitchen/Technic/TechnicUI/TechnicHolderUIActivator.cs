using UnityEngine;

public class TechnicHolderUIActivator : UIActivator {
    [SerializeField] private TechnicHolder _technicHolder;
    private TechnicRepairUI _repairUI;

    protected override void Awake() {
        base.Awake();
        _activableObject.TryGetComponent(out _repairUI);
    }

    protected override void Press() {
        if (_repairUI == null)
            return;

        if (_technicHolder.Data.IsCooking || _technicHolder.Data.IsRepairing)
            return;

        _repairUI.OpenTechnic(_technicHolder);
    }

    public void SetHolder(TechnicHolder technicHolder) {
        _technicHolder = technicHolder;
    }
}
