using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TechnicManager : MonoBehaviour, IUpgradeable
{
    [SerializeField] private TechnicHolderUI _UI;
    [SerializeField] private TechnicHolder[] _holders;
    [SerializeField] private List<Technic> _availableTechnic;

    [Header("Upgrades")]
    [SerializeField] private CoefficientUpgrade[] _technicCookSpeedUps;
    [SerializeField] private CoefficientUpgrade[] _technicRepairSpeedUps;
    [SerializeField] private CoefficientUpgrade[] _technicStrengthAdds;
    public float TechnicCookSpeed { get; private set; } = 1;
    public float TechnicRepairSpeed { get; private set; } = 1;
    public float TechnicStrength { get; private set; } = 1;

    public event Action TechnicChanged;

    private void Start()
    {
        foreach (var holder in _holders) {
            if (_availableTechnic.Contains(holder.Technic))
                holder.Activate(this);
            else
                holder.gameObject.SetActive(false);
        }
    }

    public bool HaveTechnic(Technic technic)
    {
        if (!_availableTechnic.Contains(technic))
            return false;
        var holder = FindHolderByTechic(technic);
        return !holder.IsCooking && holder.NowStrength != 0 && !holder.IsRepairing;
    }

    public void ActivateTechnic(Order order)
    {
        var technic = FindHolderByTechic(order.Food.TypeTechnic);
        technic.StartCook(order);
    }

    public void DisableTechnic(Technic typeTechnic)
    {
        var technic = FindHolderByTechic(typeTechnic);
        technic.StopCook();
    }

    public TechnicHolder FindHolderByTechic(Technic technic)
    {
        for (int i = 0; i < _holders.Length; i++)
            if (_holders[i].Technic == technic)
                return _holders[i];
        return null;
    }

    public void AddTechnic(Technic technic)
    {
        _availableTechnic.Add(technic);
        var holder = FindHolderByTechic(technic);
        holder.Activate(this);
        TechnicChanged?.Invoke();
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_technicCookSpeedUps.Contains(upgrade)) {
            var coefUpgrade = upgrade as CoefficientUpgrade;
            TechnicCookSpeed = coefUpgrade.Coefficient;
        } else if (_technicStrengthAdds.Contains(upgrade)) {
            var coefUpgrade = upgrade as CoefficientUpgrade;
            TechnicStrength = coefUpgrade.Coefficient;
        } else if (_technicRepairSpeedUps.Contains(upgrade)) {
            var coefUpgrade = upgrade as CoefficientUpgrade;
            TechnicRepairSpeed = coefUpgrade.Coefficient;
        }
    }
}
