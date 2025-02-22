using System;
using System.Linq;
using UnityEngine;

public class TechnicManager : SaveableItemManager<Technic, KitchenData>, IUpgradeable<KitchenUpgradeData> {
    [SerializeField] private TechnicHolder[] _holders;

    [Header("Upgrades")]
    [SerializeField] private CoefficientUpgrade[] _technicCookSpeedUpgrades;
    [SerializeField] private CoefficientUpgrade[] _technicRepairSpeedUpgrades;
    [SerializeField] private CoefficientUpgrade[] _technicRepairPriceUpgrades;
    [SerializeField] private CoefficientUpgrade[] _technicStrengthUpgrades;

    private KitchenUpgradeData _upgradeData;

    public event Action TechnicChanged;

    private void ActivateHolders() {
        foreach (var holder in _holders) {
            holder.ChangeState(_data.IsItemAvailable(holder.Data.Technic));
            holder.RepairChanged += delegate { TechnicChanged?.Invoke(); };
            holder.CookChanged += delegate { TechnicChanged?.Invoke(); };
        }
        TechnicChanged?.Invoke();
    }

    public bool HaveTechnic(Technic technic) {
        if (!_data.IsItemAvailable(technic))
            return false;
        TechnicHolder holder = FindHolderByTechic(technic);
        if (holder == null)
            return false;

        return holder.Accessible();
    }

    public void StartCooking(Order order) {
        TechnicHolder technic = FindHolderByTechic(order.Food.TypeTechnic);
        technic.StartCook(order);
    }

    public void EmergencyStopCooking(Order order) {
        TechnicHolder technic = FindHolderByTechic(order.Food.TypeTechnic);
        technic.Data.EmergencyStopCook();
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

    public override void LateStart() {
        base.LateStart();
        foreach (var holder in _holders)
            holder.LateStart();
    }

    public override void Bind(KitchenData data) {
        data.TechnicManager ??= new();
        _data = data.TechnicManager;

        BindHolders(data);
        ActivateHolders();
    }

    private void BindHolders(KitchenData data) {
        data.TechnicHolders ??= new TechnicHolderData[_holders.Length];

        for (int i = 0; i < data.TechnicHolders.Length; i++)
            _holders[i].Bind(data, i);
    }

    public void BindUpgrade(KitchenUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (_technicCookSpeedUpgrades.Contains(upgrade))
            _upgradeData.ChangeTechnicCookSpeed(upgrade as CoefficientUpgrade);
        else if (_technicStrengthUpgrades.Contains(upgrade))
            _upgradeData.ChangeTechnicStrength(upgrade as CoefficientUpgrade);
        else if (_technicRepairSpeedUpgrades.Contains(upgrade))
            _upgradeData.ChangeTechnicRepairSpeed(upgrade as CoefficientUpgrade);
        else if (_technicRepairPriceUpgrades.Contains(upgrade))
            _upgradeData.ChangeTechnicRepairPrice(upgrade as CoefficientUpgrade);
    }
}
