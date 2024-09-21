using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(Technic))]

public class Technic : BuyableItem {
    [SerializeField, Min(0)] private int _timeRepair;
    [SerializeField, Min(0)] private int _strength;
    [SerializeField, Min(0)] private int _priceRepair;

    protected override string _table => nameof(Technic) + "Table";

    public int TimeRepair => _timeRepair;
    public int Strength => _strength;
    public int PriceRepair => _priceRepair;
}
