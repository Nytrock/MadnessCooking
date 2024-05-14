using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TechnicStandardPanel : TechnicPanel, IUpgradeable, IBindable<KitchenData>
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _repair;
    [SerializeField] private Button _repairButton;
    [SerializeField] private Slider _cookSlider;

    private KitchenData _data;
    private TechnicCooker _cooker;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _technicStrengthShow;
    [SerializeField] private Slider _strengthShower;

    public override void UpdatePanel()
    {
        if (!_nowTechnic.TechnicData.IsCooking && _cookSlider.gameObject.activeSelf)
            UpdateInfo();

        if (!_nowTechnic.TechnicData.IsCooking) return;

        _cookSlider.value = _cooker.NowTime;
    }

    public override void UpdateInfo()
    {
        var technic = _nowTechnic.Technic;
        _cooker = _nowTechnic.GetComponent<TechnicCooker>();
        _icon.sprite = technic.Icon;
        _name.text = technic.Name;
        UpdatePanels();

        if (!_nowTechnic.TechnicData.IsCooking) {
            _repair.text = $"Repair - {technic.CostRepair}";
            _repairButton.interactable = _nowTechnic.Repairable();
        } else {
            _cookSlider.maxValue = _cooker.NeedTime;
        }

        if (_data.IsStrengthShow) {
            _strengthShower.maxValue = _nowTechnic.Technic.Strength;
            _strengthShower.value = _nowTechnic.TechnicData.NowStrength;
        }
    }

    private void UpdatePanels()
    {
        _repairButton.gameObject.SetActive(!_nowTechnic.TechnicData.IsCooking);
        _cookSlider.gameObject.SetActive(_nowTechnic.TechnicData.IsCooking);
    }

    private void ChangeStrengthShowState()
    {
        _strengthShower.gameObject.SetActive(_data.IsStrengthShow);
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _technicStrengthShow) {
            _data.IsStrengthShow = true;
            ChangeStrengthShowState();
        }
    }

    public void Bind(KitchenData data, bool isFileEmpty)
    {
        _data = data;
        ChangeStrengthShowState();
    }
}
