using Newtonsoft.Json;
using System.Threading.Tasks;
using UnityEngine;

[JsonConverter(typeof(ScriptableObjectJsonConverter))]
public class BuyableItem : ExtendedScriptableObject {
    public const string AssetMenuName = nameof(BuyableItem) + "/";

    [SerializeField] private Sprite _icon;
    [SerializeField, Min(0)] private int _price;

    protected virtual string _table => "UITable";
    private string _rawName;
    private string _rawDescription;

    public string RawName => _rawName;
    public override Sprite Icon => _icon;
    public int Price => _price;

    public void Initialize() {
        _rawName = name + ".Name";
        _rawDescription = name + ".Description";
    }

    public virtual async Task<string> GetName() {
        return await LocalizationManager.Instance.GetLocalization(_table, _rawName);
    }

    public virtual async Task<string> GetDescription() {
        return await LocalizationManager.Instance.GetLocalization(_table, _rawDescription);
    }

    public static TItem CreateTemporaryItem<TItem>(string name, Sprite icon = null)
        where TItem : BuyableItem {

        TItem item = CreateInstance<TItem>();
        item.name = name;
        item.Initialize();

        if (icon != null)
            item._icon = icon;

        return item;
    }
}
