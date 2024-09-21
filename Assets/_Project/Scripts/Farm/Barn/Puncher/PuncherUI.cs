using UnityEngine;
using UnityEngine.UI;

public class PuncherUI : MonoBehaviour, IActivable, IUpgradeable<FarmUpgradeData> {
    [SerializeField] private Puncher _puncher;
    [SerializeField] private GameObject _panel;
    [SerializeField] private CountRenderer _count;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _sliderShow;
    [SerializeField] private Slider _progressSlider;
    private FarmUpgradeData _upgradeData;

    private void Awake() {
        _puncher.FertilizerChanged += UpdateCount;
    }

    public void ChangeState() {
        _panel.SetActive(!_panel.activeSelf);

        if (_panel.activeSelf)
            UpdateProgress();
    }

    public void ChangeState(bool newState) {
        _panel.SetActive(newState);

        if (newState)
            UpdateProgress();
    }

    private void UpdateProgress() {
        if (!_upgradeData.IsPuncherProgressShow)
            return;

        _progressSlider.maxValue = _puncher.Data.NeedWaste;
        _progressSlider.value = _puncher.Data.NowWaste;
    }

    private void UpdateProgressShow() {
        _progressSlider.gameObject.SetActive(_upgradeData.IsPuncherProgressShow);
    }

    private void UpdateCount() {
        _count.UpdateCount(_puncher.Data.FertilizerCount);
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _sliderShow) {
            _upgradeData.ChangePuncherProgressShow();
            UpdateProgressShow();
        }
    }

    public void BindUpgrade(FarmUpgradeData upgradeData) {
        _upgradeData = upgradeData;
        UpdateProgressShow();
    }
}
