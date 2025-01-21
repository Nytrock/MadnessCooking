using System;
using UnityEngine;
using UnityEngine.UI;

public class TechnicRepairUI : MonoBehaviour, IUpgradeable<KitchenUpgradeData>, IActivable {
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Camera _camera;
    [SerializeField] private Button _repairButton;
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private TechnicRepairUIText _priceText;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _technicStrengthShow;
    [SerializeField] private Slider _strengthSlider;

    private TechnicHolder _nowTechnicHolder;
    private KitchenUpgradeData _upgradeData;

    public event Action<bool> StateChanged;

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
            return;
        }

        _nowTechnicHolder.StartRepair();
        ChangeState(false);
    }

    public void ChangeState(bool newState) {
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
        _priceText.SetPrice(repairPrice);
    }

    public void BindUpgrade(KitchenUpgradeData upgradeData) {
        _upgradeData = upgradeData;
        ChangeStrengthShowState();
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _technicStrengthShow) {
            _upgradeData.ChangeStrengthShow();
            ChangeStrengthShowState();
        }
    }

    private void ChangeStrengthShowState() {
        _strengthSlider.gameObject.SetActive(_upgradeData.IsStrengthShow);
    }
}
