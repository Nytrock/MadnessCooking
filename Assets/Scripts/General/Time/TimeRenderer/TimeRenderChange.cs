using UnityEngine;

public class TimeRenderChange : MonoBehaviour, IUpgradeable<GeneralUpgradeData> {
    [SerializeField] private BaseUpgrade _clockUpgrade;
    [SerializeField] private TimeRenderClock _clock;
    [SerializeField] private TimeRenderWatch _watch;
    private GeneralUpgradeData _upgradeData;

    private void Start() {
        UpdateTimeRenderer();
    }

    private void UpdateTimeRenderer() {
        _clock.gameObject.SetActive(!_upgradeData.IsUpgradedTimeRenderer);
        _watch.gameObject.SetActive(_upgradeData.IsUpgradedTimeRenderer);
    }

    public void BindUpgrade(GeneralUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _clockUpgrade) {
            _upgradeData.ChangeTimeRenderer();
            UpdateTimeRenderer();
        }
    }
}
