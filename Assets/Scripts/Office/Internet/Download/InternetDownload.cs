using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InternetDownloadRenderer))]
public class InternetDownload : MonoBehaviour, IUpgradeable<OfficeUpgradeData> {
    [SerializeField] private GameObject _panel;
    [SerializeField] private Slider _downloadBar;

    [Header("Wait borders")]
    [SerializeField] private RangeFloat _waitTime;
    [SerializeField, Min(0)] private float[] _possibleProgress;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade[] _speedUpgrades;

    private float _nowProgress;
    private float _needProgress;
    private bool _isDownloading;

    private InternetDownloadRenderer _renderer;
    private InternetPage _openingPage;
    private OfficeUpgradeData _upgradeData;

    private void Awake() {
        _renderer = GetComponent<InternetDownloadRenderer>();
    }

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

    public void StartDownload(InternetPage openingPage) {
        if (_upgradeData.IsInternetDownloadInstant) {
            openingPage.ChangeState(true);
            return;
        }

        _isDownloading = true;
        _openingPage = openingPage;

        ChangeState(true);
        _renderer.UpdateVisual(_openingPage);

        _nowProgress = 0;
        _needProgress = _waitTime.RandomValue;
        _downloadBar.maxValue = _needProgress;
    }

    private void EndDownload() {
        ChangeState(false);
        _isDownloading = false;
        _openingPage.ChangeState(true);
        _openingPage = null;
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