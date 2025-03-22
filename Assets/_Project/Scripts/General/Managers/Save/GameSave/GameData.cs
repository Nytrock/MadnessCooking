using System;

[Serializable]
public class GameData : ISaveable {
    public GeneralData General { get; set; }
    public CafeData Cafe { get; set; }
    public KitchenData Kitchen { get; set; }
    public FarmData Farm { get; set; }
    public OfficeData Office { get; set; }
    public BuyableItemManagerData<BaseUpgrade> UpgradeManager { get; set; }

    public GameData() {
        General = new();
        Cafe = new();
        Kitchen = new();
        Farm = new();
        Office = new();
    }
}
