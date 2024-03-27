using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(Technic))]

public class Technic : BuyableObject
{
    [SerializeField, Min(0)] private int _timeRepair;
    [SerializeField, Min(0)] private int _strength;
    [SerializeField, Min(0)] private int _costRepair;

    public int TimeRepair => _timeRepair;
    public int Strength => _strength;
    public int CostRepair => _costRepair;
}
