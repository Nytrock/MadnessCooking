using System;
using System.Linq;
using UnityEngine;

public class TechnicManager : MonoBehaviour, IUpgradeable, IBindable<KitchenData>
{
    [SerializeField] private TechnicHolderUI _UI;
    [SerializeField] private TechnicHolder[] _holders;
    [SerializeField] private Technic[] _defaultTechnic;
    private KitchenData _data;

    [Header("Upgrades")]
    [SerializeField] private CoefficientUpgrade[] _technicCookSpeedUps;
    [SerializeField] private CoefficientUpgrade[] _technicRepairSpeedUps;
    [SerializeField] private CoefficientUpgrade[] _technicStrengthAdds;

    public event Action TechnicChanged;

    private void ActivateHolders()
    {
        foreach (var holder in _holders)
            holder.ChangeState(_data.AvailableTechnic.Contains(holder.Technic));
        TechnicChanged?.Invoke();
    }

    public bool HaveTechnic(Technic technic)
    {
        if (!_data.AvailableTechnic.Contains(technic))
            return false;
        TechnicHolder holder = FindHolderByTechic(technic);
        return holder.Accessible();
    }

    public void ActivateTechnic(Order order)
    {
        TechnicHolder technic = FindHolderByTechic(order.Food.TypeTechnic);
        technic.StartCook(order);
    }

    public void DisableTechnic(Technic typeTechnic)
    {
        TechnicHolder technic = FindHolderByTechic(typeTechnic);
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
        _data.AvailableTechnic.Add(technic);
        TechnicHolder holder = FindHolderByTechic(technic);
        holder.ChangeState(true);
        TechnicChanged?.Invoke();
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_technicCookSpeedUps.Contains(upgrade)) {
            var coefUpgrade = upgrade as CoefficientUpgrade;
            _data.TechnicCookSpeed = coefUpgrade.Coefficient;
        } else if (_technicStrengthAdds.Contains(upgrade)) {
            var coefUpgrade = upgrade as CoefficientUpgrade;
            _data.TechnicStrength = coefUpgrade.Coefficient;
        } else if (_technicRepairSpeedUps.Contains(upgrade)) {
            var coefUpgrade = upgrade as CoefficientUpgrade;
            _data.TechnicRepairSpeed = coefUpgrade.Coefficient;
        }
    }

    public void Bind(KitchenData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty) {
            _data.AllTechnic = new SerializableTechnic[_holders.Length];
            _data.AvailableTechnic = _defaultTechnic.ToList();
        }

        ActivateHolders();
        BindHolders(isFileEmpty);
    }

    private void BindHolders(bool isFileEmpty)
    {
        for (int i = 0; i < _data.AllTechnic.Length; i++) {
            _holders[i].Bind(_data, i, isFileEmpty);
        }
    }
}
