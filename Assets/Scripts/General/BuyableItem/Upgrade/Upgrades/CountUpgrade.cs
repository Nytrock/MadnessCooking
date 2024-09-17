using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(CountUpgrade))]
public class CountUpgrade : BaseUpgrade {
    [SerializeField, Min(0)] private int _count;

    public int Count => _count;

    protected override string GetDescription() {
        Dictionary<string, string> arguments = new() {
            ["count"] = _count.ToString()
        };
        return LocalizationManager.Instance.GetLocalization(_table, name + ".Description", arguments);
    }
}
