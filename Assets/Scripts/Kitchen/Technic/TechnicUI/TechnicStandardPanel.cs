using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TechnicStandardPanel : TechnicPanel, IUpgradeable
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _repair;
    [SerializeField] private Button _repairButton;
    [SerializeField] private Slider _cookSlider;

    private TechnicCooker _cooker;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _technicStrengthShow;
    [SerializeField] private Slider _strengthShower;
    private bool _isStrengthShow;

    private void Start()
    {
        ChangeStrengthShowState();
    }

    public override void UpdatePanel()
    {
        if (!_nowTechnic.IsCooking && _cookSlider.gameObject.activeSelf)
            UpdateInfo();

        if (!_nowTechnic.IsCooking) return;

        _cookSlider.value = _cooker.NowTime;
    }

    public override void UpdateInfo()
    {
        var technic = _nowTechnic.Technic;
        _cooker = _nowTechnic.GetComponent<TechnicCooker>();
        _icon.sprite = technic.Icon;
        _name.text = technic.Name;
        UpdatePanels();

        if (!_nowTechnic.IsCooking) {
            _repair.text = $"Repair - {technic.CostRepair}";
            _repairButton.interactable =
                _nowTechnic.NowStrength != technic.Strength && MoneyManager.instance.MoneyAmount >= technic.CostRepair;
        } else {
            _cookSlider.maxValue = _cooker.NeedTime;
        }

        if (_isStrengthShow) {
            _strengthShower.maxValue = _nowTechnic.Technic.Strength;
            _strengthShower.value = _nowTechnic.NowStrength;
        }
    }

    private void UpdatePanels()
    {
        _repairButton.gameObject.SetActive(!_nowTechnic.IsCooking);
        _cookSlider.gameObject.SetActive(_nowTechnic.IsCooking);
    }

    private void ChangeStrengthShowState()
    {
        _strengthShower.gameObject.SetActive(_isStrengthShow);
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _technicStrengthShow) {
            _isStrengthShow = true;
            ChangeStrengthShowState();
        }
    }
}
