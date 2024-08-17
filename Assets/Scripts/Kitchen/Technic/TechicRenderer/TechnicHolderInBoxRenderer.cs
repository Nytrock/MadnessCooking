using UnityEngine;

public class TechnicHolderInBoxRenderer : TechnicHolderRenderer {
    [SerializeField] private SpriteChanger _box;

    public override void ChangeState(bool newState) {
        base.ChangeState(newState);
        _box.ChangeState(newState);
    }
}
