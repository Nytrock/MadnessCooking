using Newtonsoft.Json;
using UnityEngine;

[JsonConverter(typeof(ScriptableObjectConverter))]
public abstract class BuyableItem : ExtendedScriptableObject {
    public const string AssetMenuName = nameof(BuyableItem) + "/";

    [SerializeField] private Sprite _icon;
    [SerializeField, Min(0)] private int _price;

    protected abstract string _table { get; }

    public string Name => GetName();
    public string Description => GetDescription();
    public override Sprite Icon => _icon;
    public int Price => _price;

    protected virtual string GetName() {
        return LocalizationManager.Instance.GetLocalization(_table, name + ".Name");
    }

    protected virtual string GetDescription() {
        return LocalizationManager.Instance.GetLocalization(_table, name + ".Description");
    }

    public static TItem CreateTemporaryItem<TItem>(string name)
        where TItem : BuyableItem {

        TItem item = CreateInstance<TItem>();
        item.name = name;
        return item;
    }
}
