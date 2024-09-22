using UnityEngine;

public class UpgradeRenderer : MonoBehaviour {
    [SerializeField] protected BaseUpgrade _upgrade;
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private VisualChanger _changer;

    private void Awake() {
        _upgradeManager.ItemAdded += CheckAddedUpgrade;
        ChangeState(false);
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        ChangeState(upgrade == _upgrade);
    }

    protected virtual void ChangeState(bool newState) {
        _changer.ChangeState(newState);
    }
}
