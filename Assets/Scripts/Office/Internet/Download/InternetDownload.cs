using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InternetDownload : MonoBehaviour, IUpgradeable<OfficeUpgradeData> {
    [SerializeField] private GameObject _panel;
    [SerializeField] private Slider _downloadBar;

    [Header("Wait borders")]
    [SerializeField, Min(0)] private float _minWait;
    [SerializeField, Min(0)] private float _maxWait;
    [SerializeField, Min(0)] private float[] _possibleProgress;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade[] _speedUpgrades;

    private float _nowProgress;
    private float _needProgress;
    private bool _isDownloading;
    private BaseShop _shop;
    private OfficeUpgradeData _upgradeData;

    private void Start() {
        ChangeState(false);
    }

    private void Update() {
        if (!_isDownloading)
            return;

        if (_nowProgress < _needProgress)
            _nowProgress += Time.deltaTime * _upgradeData.InternetDownloadSpeed *
                _possibleProgress[Random.Range(0, _possibleProgress.Length)];
        else
            EndDownload();
        _downloadBar.value = _nowProgress;
    }

    private void ChangeState(bool newValue) {
        _panel.SetActive(newValue);
    }

    public void StartDownload(BaseShop openingShop) {
        if (_upgradeData.IsInternetDownloadInstant) {
            openingShop.ChangeShopState(true);
            return;
        }

        ChangeState(true);
        _needProgress = Random.Range(_minWait, _maxWait);
        _downloadBar.maxValue = _needProgress;
        _isDownloading = true;
        _shop = openingShop;
    }

    private void EndDownload() {
        ChangeState(false);
        _isDownloading = false;
        _nowProgress = 0;
        _shop.ChangeShopState(true);
        _shop = null;
    }

    public void BindUpgrade(OfficeUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (_speedUpgrades.Contains(upgrade)) {
            var coefUpgrade = upgrade as CoefficientUpgrade;
            if (coefUpgrade != null)
                _upgradeData.ChangeInternetDownloadSpeed(coefUpgrade);
            else
                _upgradeData.ChangeInternetDownloadInstant();
        }
    }
}
