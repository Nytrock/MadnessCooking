using Newtonsoft.Json;
using UnityEngine;

[JsonConverter(typeof(ScriptableObjectJsonConverter))]
public class BuyableItem : ExtendedScriptableObject {
    public const string AssetMenuName = nameof(BuyableItem) + "/";

    [SerializeField] private Sprite _icon;
    [SerializeField, Min(0)] private int _price;

    protected virtual string _table => "UITable";
    private string _rawName;
    private string _rawDescription;

    public string Name => GetName();
    public string RawName => _rawName;
    public string Description => GetDescription();
    public string RawDescription => _rawDescription;
    public override Sprite Icon => _icon;
    public int Price => _price;

    public override void Initialize() {
        _rawName = name + ".Name";
        _rawDescription = name + ".Description";
    }

    protected virtual string GetName() {
        return LocalizationManager.Instance.GetLocalization(_table, RawName);
    }

    protected virtual string GetDescription() {
        return LocalizationManager.Instance.GetLocalization(_table, RawDescription);
    }

    public static TItem CreateTemporaryItem<TItem>(string name, Sprite icon = null)
        where TItem : BuyableItem {

        TItem item = CreateInstance<TItem>();
        item.name = name;
        item.Initialize();

        if (icon != null)
            item.SetIcon(icon);

        return item;
    }

    private void SetIcon(Sprite icon) {
        _icon = icon;
    }
}
