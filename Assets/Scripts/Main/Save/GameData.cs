using System;

[Serializable] public class GameData
{
    public MainData Main;
    public CafeData Cafe;
    public KitchenData Kitchen;
    public FarmData Farm;
    public OfficeData Office;

    public GameData() { 
        Main = new MainData();
        Cafe = new CafeData();
        Kitchen = new KitchenData();
        Farm = new FarmData();
        Office = new OfficeData();
    }
}
