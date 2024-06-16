using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TechnicStandardPanel : TechnicPanel, IUpgradeable<KitchenUpgradeData> {
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _repairText;
    [SerializeField] private Button _repairButton;
    [SerializeField] private Slider _cookSlider;

    private KitchenUpgradeData _upgradeData;
    private TechnicCooker _cooker;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _technicStrengthShow;
    [SerializeField] private Slider _strengthShower;

    public override void UpdatePanel() {
        if (!_nowTechnic.TechnicData.IsCooking && _cookSlider.gameObject.activeSelf)
            UpdateInfo();

        if (!_nowTechnic.TechnicData.IsCooking) return;

        _cookSlider.value = _cooker.NowTime;
    }

    public override void UpdateInfo() {
        Technic technic = _nowTechnic.Technic;
        _cooker = _nowTechnic.GetComponent<TechnicCooker>();
        _icon.sprite = technic.Icon;
        _nameText.text = technic.Name;
        UpdatePanels();

        if (!_nowTechnic.TechnicData.IsCooking) {
            _repairText.text = $"Repair - {technic.PriceRepair}";
            _repairButton.interactable = _nowTechnic.Repairable();
        } else {
            _cookSlider.maxValue = _cooker.NeedTime;
        }

        if (_upgradeData.IsStrengthShow) {
            _strengthShower.maxValue = _nowTechnic.Technic.Strength;
            _strengthShower.value = _nowTechnic.TechnicData.NowStrength;
        }
    }

    private void UpdatePanels() {
        _repairButton.gameObject.SetActive(!_nowTechnic.TechnicData.IsCooking);
        _cookSlider.gameObject.SetActive(_nowTechnic.TechnicData.IsCooking);
    }

    private void ChangeStrengthShowState() {
        _strengthShower.gameObject.SetActive(_upgradeData.IsStrengthShow);
    }

    public void BindUpgrade(KitchenUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _technicStrengthShow) {
            _upgradeData.ChangeStrengthShow();
            ChangeStrengthShowState();
        }
    }
}
