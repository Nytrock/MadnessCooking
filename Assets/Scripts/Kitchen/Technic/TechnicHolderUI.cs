using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TechnicHolderUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _repair;
    [SerializeField] private Button _repairButton;
    [SerializeField] private Slider _cookSlider;
    private TechnicHolder _nowTechnic;

    private void Start()
    {
        _panel.SetActive(false);
    }

    private void Update()
    {
        if (_nowTechnic == null) return;

        if (_nowTechnic.IsFree && _cookSlider.gameObject.activeSelf)
            UpdatePanelInfo();

        if (_nowTechnic.IsFree) return;

        _cookSlider.value = _nowTechnic.NowTime;
    }

    public void OpenTechnic(TechnicHolder technic)
    {
        if (technic == _nowTechnic) {
            _panel.SetActive(!_panel.activeSelf);
        } else {
            _panel.SetActive(true);
            _panel.transform.position = technic.UITarget.position;
            _nowTechnic = technic;
        }
        UpdatePanelInfo();
    }

    private void UpdatePanelInfo()
    {
        var technic = _nowTechnic.Technic;
        _icon.sprite = technic.MiniSprite;
        _name.text = technic.Name;
        UpdatePanels();

        if (_nowTechnic.IsFree) {
            _repair.text = $"Repair - {technic.CostRepair}";
            _repairButton.interactable =
                _nowTechnic.NowStrength != technic.Strength && MoneyManager.instance.MoneyAmount >= technic.CostRepair;
        } else {
            _cookSlider.maxValue = _nowTechnic.CookTime;
        }
    }

    private void UpdatePanels() {
        _repairButton.gameObject.SetActive(_nowTechnic.IsFree);
        _cookSlider.gameObject.SetActive(!_nowTechnic.IsFree);
    }
}
