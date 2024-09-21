using UnityEngine;

public class UpgradeRenderer : MonoBehaviour {
    [SerializeField] private BaseUpgrade _upgrade;
    [SerializeField] private UpgradeManager _upgradeManager;

    private void Awake() {
        _upgradeManager.ItemAdded += CheckAddedUpgrade;
        ChangeState(false);
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _upgrade)
            ChangeState(true);
    }

    protected virtual void ChangeState(bool newState) {
        gameObject.SetActive(newState);
    }
}
