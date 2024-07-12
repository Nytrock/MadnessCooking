using System;

[Serializable]
public class GameData {
    public GeneralData General;
    public CafeData Cafe;
    public KitchenData Kitchen;
    public FarmData Farm;
    public OfficeData Office;

    public GameData() {
        General = new GeneralData();
        Cafe = new CafeData();
        Kitchen = new KitchenData();
        Farm = new FarmData();
        Office = new OfficeData();
    }
}
