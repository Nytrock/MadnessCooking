using System;

[Serializable]
public class FarmData : ISaveable {
    public FarmUpgradeData UpgradeData { get; set; }
    public BuyableItemManagerData<Ingredient> IngredientManager { get; set; }
    public BuyableItemManagerData<BedType> BedTypeManager { get; set; }
    public WheatManagerData WheatManager { get; set; }
    public FarmBedManagerData FarmBedGroups { get; set; }
    public FarmShopData FarmShop { get; set; }
    public HoldAddData FarmWell { get; set; }
    public NeedHoldAddData Cow { get; set; }
    public PuncherData Puncher { get; set; }
    public NeedHoldAddData FlourMill { get; set; }
    public ChickensData Chickens { get; set; }
    public IngredientStorageData Car { get; set; }
    public CarWaitManagerData CarWaitManager { get; set; }
}
