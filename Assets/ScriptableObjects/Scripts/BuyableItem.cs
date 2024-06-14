using UnityEngine;

public abstract class BuyableItem : ScriptableObject {
    public const string AssetMenuName = nameof(BuyableItem) + "/";

    [SerializeField] private Sprite _icon;
    [SerializeField, Min(0)] private int _price;

    public string Name => name + ".Name";
    public string Description => name + ".Description";
    public Sprite Icon => _icon;
    public int Price => _price;
}
