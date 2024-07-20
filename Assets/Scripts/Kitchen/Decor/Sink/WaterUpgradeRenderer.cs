using UnityEngine;

public class WaterUpgradeRenderer : MonoBehaviour, IUpgradeable<KitchenUpgradeData> {
    [SerializeField] private BaseUpgrade _waterUpgrade;
    private KitchenUpgradeData _upgradeData;

    public void BindUpgrade(KitchenUpgradeData upgradeData) {
        _upgradeData = upgradeData;
        UpdateState();
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _waterUpgrade) {
            _upgradeData.ChangeWaterAvailable();
            UpdateState();
        }
    }

    private void UpdateState() {
        gameObject.SetActive(_upgradeData.IsWaterAvailable);
    }
}
