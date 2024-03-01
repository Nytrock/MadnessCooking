using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(Technic))]

public class Technic : BuyableObject
{
    [SerializeField] private int _timeRepair;
    [SerializeField] private int _strength;
    [SerializeField] private int _costRepair;

    public int TimeRepair => _timeRepair;
    public int Strength => _strength;
    public int CostRepair => _costRepair;
}
