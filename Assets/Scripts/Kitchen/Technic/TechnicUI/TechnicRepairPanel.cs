using System;
using UnityEngine;
using UnityEngine.UI;

public class TechnicRepairPanel : TechnicPanel
{
    [SerializeField] private Slider _repairSlider;
    [SerializeField] private Image _icon;

    private TechnicRepair _repair;

    public event Action RepairEnded;

    public override void UpdatePanel()
    {
        if (!_nowTechnic.IsRepairing && enabled)
            RepairEnded?.Invoke();

        _repairSlider.value = _repair.NowTime;
    }

    public override void UpdateInfo()
    {
        _repair = _nowTechnic.GetComponent<TechnicRepair>();
        _repairSlider.maxValue = _repair.NeedTime;
    }
}
