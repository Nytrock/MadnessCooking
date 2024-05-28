using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InternetDownload : MonoBehaviour, IUpgradeable, IBindable<OfficeData> {
    [SerializeField] private GameObject _panel;
    [SerializeField] private Slider _downloadBar;

    [Header("Wait borders")]
    [SerializeField, Min(0)] private float _minWait;
    [SerializeField, Min(0)] private float _maxWait;

    [Header("Upgrades")]
    [SerializeField] private GraphUpgrade[] _speedUpgrades;

    private float _nowProgress;
    private float _needProgress;
    private bool _isDownloading;
    private BaseShop _shop;
    private OfficeData _data;

    private void LateStart() {
        ChangeState(false);
    }

    private void Update() {
        if (!_isDownloading)
            return;

        if (_nowProgress < _needProgress)
            _nowProgress += Time.deltaTime * _data.InternetDownloadSpeed;
        else
            EndDownload();
        _downloadBar.value = _nowProgress;
    }

    private void ChangeState(bool newValue) {
        _panel.SetActive(newValue);
    }

    public void StartDownload(BaseShop openingShop) {
        if (_data.IsInternetDownloadInstant) {
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

    public void CheckUpgrade(BaseUpgrade upgrade) {
        if (_speedUpgrades.Contains(upgrade)) {
            var coefUpgrade = upgrade as CoefficientUpgrade;
            if (coefUpgrade != null) {
                _data.InternetDownloadSpeed = coefUpgrade.Coefficient;
            } else {
                _data.IsInternetDownloadInstant = true;
            }
        }
    }

    public void Bind(OfficeData data, bool isFileEmpty) {
        _data = data;
        LateStart();
    }
}
