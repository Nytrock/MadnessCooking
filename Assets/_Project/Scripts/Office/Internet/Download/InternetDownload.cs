using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using MadnessCooking.General;

namespace MadnessCooking.Office {
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

        private InternetDownloadRenderer _renderer;
        private InternetPage _openingPage;
        private OfficeUpgradeData _upgradeData;

        public event Action LoadingUpdated;
        public event Action LoadingEnded;

        private void Awake() {
            _renderer = GetComponent<InternetDownloadRenderer>();
        }

        private void Start() {
            ChangeState(false);
        }

        private void Update() {
            _downloadBar.value = _nowProgress;
        }

        private IEnumerator Download() {
            float deltaTime = FpsManager.REFERENCE_DELTA_TIME;
            WaitForSeconds waitTime = new(deltaTime);

            while (_nowProgress < _needProgress) {
                float progress = _possibleProgress.GetRandom();
                _nowProgress += deltaTime * _upgradeData.InternetDownloadSpeed * progress;
                if (progress > 0)
                    LoadingUpdated?.Invoke();
                yield return waitTime;
            }

            EndDownload();
        }

        private void ChangeState(bool newValue) {
            _panel.SetActive(newValue);
        }

        public void StartDownload(InternetPage openingPage) {
            if (_upgradeData.IsInternetDownloadInstant) {
                openingPage.ChangeState(true);
                return;
            }

            _openingPage = openingPage;

            ChangeState(true);
            _renderer.UpdateVisual(_openingPage);

            _nowProgress = 0;
            _needProgress = _waitTime.RandomValue;
            _downloadBar.maxValue = _needProgress;

            StartCoroutine(nameof(Download));
        }

        private void EndDownload() {
            StopCoroutine(nameof(Download));
            ChangeState(false);
            _openingPage.ChangeState(true);
            _openingPage = null;

            LoadingEnded?.Invoke();
        }

        public void StopDownload() {
            StopCoroutine(nameof(Download));
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
}