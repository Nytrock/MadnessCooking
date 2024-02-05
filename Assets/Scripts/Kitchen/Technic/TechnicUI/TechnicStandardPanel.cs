using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TechnicStandardPanel : TechnicPanel
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _repair;
    [SerializeField] private Button _repairButton;
    [SerializeField] private Slider _cookSlider;

    private TechnicCooker _cooker;

    public override void UpdatePanel()
    {
        if (_nowTechnic.IsFree && _cookSlider.gameObject.activeSelf)
            UpdateInfo();

        if (_nowTechnic.IsFree) return;

        _cookSlider.value = _cooker.NowTime;
    }

    public override void UpdateInfo()
    {
        var technic = _nowTechnic.Technic;
        _cooker = _nowTechnic.GetComponent<TechnicCooker>();
        _icon.sprite = technic.MiniSprite;
        _name.text = technic.Name;
        UpdatePanels();

        if (_nowTechnic.IsFree) {
            _repair.text = $"Repair - {technic.CostRepair}";
            _repairButton.interactable =
                _nowTechnic.NowStrength != technic.Strength && MoneyManager.instance.MoneyAmount >= technic.CostRepair;
        } else {
            _cookSlider.maxValue = _cooker.NeedTime;
        }
    }

    private void UpdatePanels()
    {
        _repairButton.gameObject.SetActive(_nowTechnic.IsFree);
        _cookSlider.gameObject.SetActive(!_nowTechnic.IsFree);
    }
}
