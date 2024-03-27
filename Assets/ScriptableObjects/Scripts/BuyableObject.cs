using UnityEngine;

public abstract class BuyableObject : ScriptableObject
{
    public const string AssetMenuName = nameof(BuyableObject) + "/";

    [SerializeField] private Sprite _icon;
    [SerializeField, Min(0)] private int _cost;

    public string Name => name + ".Name";
    public string Description => name + ".Description";
    public Sprite Icon => _icon;
    public int Cost => _cost;
}
