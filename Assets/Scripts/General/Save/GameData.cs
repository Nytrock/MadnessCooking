using System;

[Serializable]
public class GameData {
    public GeneralData Main;
    public CafeData Cafe;
    public KitchenData Kitchen;
    public FarmData Farm;
    public OfficeData Office;

    public GameData() {
        Main = new GeneralData();
        Cafe = new CafeData();
        Kitchen = new KitchenData();
        Farm = new FarmData();
        Office = new OfficeData();
    }
}
