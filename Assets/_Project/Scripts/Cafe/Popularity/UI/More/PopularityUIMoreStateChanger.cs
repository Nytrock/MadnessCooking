using UnityEngine;

public class PopularityUIMoreStateChanger : HoverObjectStateChanger {
    [SerializeField] private PopularityUIMore _moreUI;

    protected override void ActivateHoverObject() {
        base.ActivateHoverObject();
        _moreUI.ChangeMode(true);
    }

    protected override void DisableHoverObject() {
        base.DisableHoverObject();
        _moreUI.ChangeMode(false);
    }
}
