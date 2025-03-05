using UnityEngine;

public class UpgradeRenderer : MonoBehaviour {
    [SerializeField] protected BaseUpgrade _upgrade;
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private VisualChanger _changer;

    private void Awake() {
        _upgradeManager.ItemAdded += CheckAddedUpgrade;
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _upgrade)
            ChangeState(true);
    }

    protected virtual void ChangeState(bool newState) {
        _changer.ChangeState(newState);
    }
}
