using UnityEngine;

public class TechnicHolderInBox : TechnicHolder {
    [SerializeField] private KitchenBox _box;

    public override void ChangeState(bool newState) {
        base.ChangeState(newState);
        if (newState)
            _box.OpenBox();
    }
}
