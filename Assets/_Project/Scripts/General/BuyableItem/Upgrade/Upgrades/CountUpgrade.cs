using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(CountUpgrade))]
public class CountUpgrade : BaseUpgrade {
    [SerializeField, Min(0)] private int _count;

    public int Count => _count;

    public override async Task<string> GetDescription() {
        Dictionary<string, string> arguments = new() {
            ["count"] = _count.ToString()
        };
        return await LocalizationManager.Instance.GetLocalization(_table, name + ".Description", arguments);
    }
}
