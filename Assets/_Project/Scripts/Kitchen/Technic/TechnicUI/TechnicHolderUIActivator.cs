using UnityEngine;

public class TechnicHolderUIActivator : UIActivator {
    [SerializeField] private TechnicHolder _technicHolder;
    [SerializeField] private UIActivator _orderActivator;
    private TechnicRepairUI _repairUI;

    protected override void Awake() {
        base.Awake();
        _activableObject.TryGetComponent(out _repairUI);
        _orderActivator.StateChanged += delegate { CloseUI(); };
    }

    protected override void Press() {
        if (_repairUI == null)
            return;

        if (_technicHolder.Data.IsCooking || _technicHolder.Data.IsRepairing)
            return;

        _repairUI.OpenTechnic(_technicHolder);
    }
}
