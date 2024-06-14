using System;
using System.Linq;
using UnityEngine;

public class TechnicManager : BuyableItemManager<Technic>, IUpgradeable, IBindable<KitchenData> {
    [SerializeField] private TechnicHolderUI _UI;
    [SerializeField] private TechnicHolder[] _holders;

    [Header("Upgrades")]
    [SerializeField] private CoefficientUpgrade[] _technicCookSpeedUps;
    [SerializeField] private CoefficientUpgrade[] _technicRepairSpeedUps;
    [SerializeField] private CoefficientUpgrade[] _technicStrengthAdds;

    public KitchenData _tempDataDontForgetToDelete;

    public event Action TechnicChanged;

    private void ActivateHolders() {
        foreach (var holder in _holders)
            holder.ChangeState(_data.IsItemAvailable(holder.Technic));
        TechnicChanged?.Invoke();
    }

    public bool HaveTechnic(Technic technic) {
        if (!_data.IsItemAvailable(technic))
            return false;
        TechnicHolder holder = FindHolderByTechic(technic);
        return holder.Accessible();
    }

    public void ActivateTechnic(Order order) {
        TechnicHolder technic = FindHolderByTechic(order.Food.TypeTechnic);
        technic.StartCook(order);
    }

    public void DisableTechnic(Technic typeTechnic) {
        TechnicHolder technic = FindHolderByTechic(typeTechnic);
        technic.StopCook();
    }

    public TechnicHolder FindHolderByTechic(Technic technic) {
        for (int i = 0; i < _holders.Length; i++)
            if (_holders[i].Technic == technic)
                return _holders[i];
        return null;
    }

    public override void AddItem(Technic technic) {
        base.AddItem(technic);
        TechnicHolder holder = FindHolderByTechic(technic);
        holder.ChangeState(true);
        TechnicChanged?.Invoke();
    }

    public void CheckUpgrade(BaseUpgrade upgrade) {
        if (_technicCookSpeedUps.Contains(upgrade)) {
            var coefUpgrade = upgrade as CoefficientUpgrade;
            _tempDataDontForgetToDelete.TechnicCookSpeed = coefUpgrade.Coefficient;
        } else if (_technicStrengthAdds.Contains(upgrade)) {
            var coefUpgrade = upgrade as CoefficientUpgrade;
            _tempDataDontForgetToDelete.TechnicStrength = coefUpgrade.Coefficient;
        } else if (_technicRepairSpeedUps.Contains(upgrade)) {
            var coefUpgrade = upgrade as CoefficientUpgrade;
            _tempDataDontForgetToDelete.TechnicRepairSpeed = coefUpgrade.Coefficient;
        }
    }

    public void Bind(KitchenData data, bool isFileEmpty) {
        _tempDataDontForgetToDelete = data;
        if (isFileEmpty)
            data.TechnicManager = new(_defaultItems);
        _data = data.TechnicManager;

        ActivateHolders();
        BindHolders(data, isFileEmpty);
    }

    private void BindHolders(KitchenData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.TechnicHolders = new TechnicHolderData[_holders.Length];

        for (int i = 0; i < data.TechnicHolders.Length; i++)
            _holders[i].Bind(data, i, isFileEmpty);
    }
}
