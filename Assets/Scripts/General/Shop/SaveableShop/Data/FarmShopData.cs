using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class FarmShopData : ShopData<BaseUpgrade> {
    [SerializeField, JsonProperty] private List<ConsumableUpgradeHolder> _upgradeHolders;

    public FarmShopData(IEnumerable<BaseUpgrade> defaultItems, IEnumerable<ConsumableUpgrade> comsumableUpgrades) : base(defaultItems) {
        _upgradeHolders = new();

        if (comsumableUpgrades == null)
            return;

        foreach (var upgrade in comsumableUpgrades)
            _upgradeHolders.Add(new(upgrade));
    }

    public bool AddCountAndCheckMax(ConsumableUpgrade addedUpgrade) {
        foreach (var holder in _upgradeHolders) {
            if (holder.ConsumableUpgrade == addedUpgrade) {
                holder.AddCount();
                if (holder.IsMax)
                    _upgradeHolders.Remove(holder);
                return holder.IsMax;
            }
        }

        throw new ArgumentNullException($"No holder for consumable upgrade {addedUpgrade.name}");
    }
}
