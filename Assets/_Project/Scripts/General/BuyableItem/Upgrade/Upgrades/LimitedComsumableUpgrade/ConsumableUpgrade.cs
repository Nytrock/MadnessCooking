using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(ConsumableUpgrade))]
public class ConsumableUpgrade : BaseUpgrade {
    [SerializeField, Min(1)] private int _maxCount;

    public int MaxCount => _maxCount;

    protected override string GetDescription() {
        Dictionary<string, string> arguments = new() {
            ["maxCount"] = _maxCount.ToString()
        };
        return LocalizationManager.Instance.GetLocalization(_table, name + ".Description", arguments);
    }
}
