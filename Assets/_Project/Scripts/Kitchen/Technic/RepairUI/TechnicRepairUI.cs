using System;
using UnityEngine;
using UnityEngine.UI;

public class TechnicRepairUI : MonoBehaviour, IActivable {
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Camera _camera;
    [SerializeField] private Button _repairButton;
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private TechnicRepairUIText _priceText;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _technicStrengthShow;
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private Slider _strengthSlider;

    private TechnicHolder _nowTechnicHolder;
    private bool _isStrengthShow;

    public event Action<bool> StateChanged;

    private void Awake() {
        _upgradeManager.ItemAdded += CheckAddedUpgrade;
        UpdateStrengthShowState();
    }

    private void Start() {
        _panel.SetActive(false);
    }

    public void OpenTechnic(TechnicHolder technicHolder) {
        if (technicHolder == _nowTechnicHolder) {
            ChangeState(false);
        } else {
            _targetPoint.position = technicHolder.UITarget.position;
            _nowTechnicHolder = technicHolder;
            ChangeState(true);
        }
    }

    public void StartRepair() {
        if (_tutorialManager.IsWork) {
            _tutorialManager.NextTutorialPart();
            ChangeState(false);
            return;
        }

        _nowTechnicHolder.StartRepair();
        ChangeState(false);
    }

    public void ChangeState(bool newState) {
        if (newState == _panel.activeSelf && _panel.activeSelf == false)
            return;

        _panel.SetActive(newState);
        StateChanged?.Invoke(newState);

        if (newState)
            UpdateInfo();
        else
            _nowTechnicHolder = null;
    }

    public void ChangeState() {
        _panel.SetActive(!_panel.activeSelf);

        if (_panel.activeSelf)
            UpdateInfo();
        else
            _nowTechnicHolder = null;
    }

    private void UpdateInfo() {
        float maxStrength = _nowTechnicHolder.Data.Technic.Strength;
        float nowStrength = _nowTechnicHolder.Data.NowStrength;

        _repairButton.interactable = _nowTechnicHolder.Repairable() || _tutorialManager.IsWork;
        _strengthSlider.maxValue = maxStrength;
        _strengthSlider.value = nowStrength;

        int repairPrice = _nowTechnicHolder.Data.GetRepairPrice();
        _priceText.SetPrice(repairPrice, _tutorialManager.IsWork);
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _technicStrengthShow) {
            _isStrengthShow = true;
            UpdateStrengthShowState();
        }
    }

    private void UpdateStrengthShowState() {
        _strengthSlider.gameObject.SetActive(_isStrengthShow);
    }
}
