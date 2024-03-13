using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InternetDownload : MonoBehaviour, IUpgradeable
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Slider _downloadBar;

    [Header("Wait borders")]
    [SerializeField] private float _minWait;
    [SerializeField] private float _maxWait;

    [Header("Upgrades")]
    [SerializeField] private ProgressUpgrade[] _speedUpgrades;
    private float _speed = 1;
    private bool _isInstant;

    private float _nowProgress;
    private float _needProgress;
    private bool _isDownloading;
    private BaseShop _shop;

    private void Start()
    {
        ChangeState(false);
    }

    private void Update()
    {
        if (!_isDownloading)
            return;

        if (_nowProgress < _needProgress)
            _nowProgress += Time.deltaTime * _speed;
        else
            EndDownload();
        _downloadBar.value = _nowProgress;
    }

    private void ChangeState(bool newValue)
    {
        _panel.SetActive(newValue);
    }

    public void StartDownload(BaseShop openingShop)
    {
        if (_isInstant) {
            openingShop.ChangeShopState(true);
            return;
        }

        ChangeState(true);
        _needProgress = Random.Range(_minWait, _maxWait);
        _downloadBar.maxValue = _needProgress;
        _isDownloading = true;
        _shop = openingShop;
    }

    private void EndDownload()
    {
        ChangeState(false);
        _isDownloading = false;
        _nowProgress = 0;
        _shop.ChangeShopState(true);
        _shop = null;
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_speedUpgrades.Contains(upgrade)) {
            var coefUpgrade = upgrade as CoefficientUpgrade;
            if (coefUpgrade != null) {
                _speed = coefUpgrade.Coefficient;
            } else {
                _isInstant = true;
            }
        }
    }
}
