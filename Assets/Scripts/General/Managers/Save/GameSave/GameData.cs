using System;

[Serializable]
public class GameData : ISaveable {
    public GeneralData General;
    public CafeData Cafe;
    public KitchenData Kitchen;
    public FarmData Farm;
    public OfficeData Office;
    public BuyableItemManagerData<BaseUpgrade> UpgradeManager;

    public GameData() {
        General = new();
        Cafe = new();
        Kitchen = new();
        Farm = new();
        Office = new();
        UpgradeManager = new();
    }
}
