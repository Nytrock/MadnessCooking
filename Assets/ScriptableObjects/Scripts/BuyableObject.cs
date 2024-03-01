using UnityEngine;

public abstract class BuyableObject : ScriptableObject
{
    public const string AssetMenuName = nameof(BuyableObject) + "/";

    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [TextArea, SerializeField] private string _description;
    [SerializeField] private int _cost;

    public Sprite Icon => _icon;
    public string Name => _name;
    public string Description => _description;
    public int Cost => _cost;
}
