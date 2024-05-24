using System;
using UnityEngine;
using UnityEngine.UI;

public class TechnicRepairPanel : TechnicPanel
{
    [SerializeField] private Slider _repairSlider;
    [SerializeField] private Image _icon;

    private TechnicRepairer _repair;

    public event Action RepairEnded;

    public override void UpdatePanel()
    {
        if (!_nowTechnic.TechnicData.IsRepairing && enabled)
            RepairEnded?.Invoke();

        _repairSlider.value = _repair.NowTime;
    }

    public override void UpdateInfo()
    {
        _repair = _nowTechnic.GetComponent<TechnicRepairer>();
        _repairSlider.maxValue = _repair.NeedTime;
    }
}
