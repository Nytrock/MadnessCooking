using System;
using UnityEngine;
using UnityEngine.UI;

public class PuncherUI : MonoBehaviour, IStateable {
    [SerializeField] private Puncher _puncher;
    [SerializeField] private GameObject _panel;
    [SerializeField] private CountRenderer _count;

    [Header("Upgrades")]
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private BaseUpgrade _sliderShow;
    [SerializeField] private Slider _progressSlider;

    private bool _isProgressShow = false;

    public event Action<bool> StateChanged;

    private void Awake() {
        _puncher.FertilizerChanged += UpdateCount;
        _upgradeManager.ItemAdded += CheckAddedUpgrade;
        UpdateProgressShow();
    }

    public void ChangeState(bool newState) {
        _panel.SetActive(newState);

        if (newState)
            UpdateProgress();
        StateChanged?.Invoke(newState);
    }

    private void UpdateProgress() {
        if (!_isProgressShow)
            return;

        _progressSlider.maxValue = _puncher.Data.NeedWaste;
        _progressSlider.value = _puncher.Data.NowWaste;
    }

    private void UpdateProgressShow() {
        _progressSlider.gameObject.SetActive(_isProgressShow);
    }

    private void UpdateCount() {
        _count.UpdateCount(_puncher.Data.FertilizerCount);
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _sliderShow) {
            _isProgressShow = true;
            UpdateProgressShow();
        }
    }
}
