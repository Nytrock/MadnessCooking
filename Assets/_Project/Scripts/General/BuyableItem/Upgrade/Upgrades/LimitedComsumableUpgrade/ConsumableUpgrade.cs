using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MadnessCooking.General {
    [CreateAssetMenu(menuName = AssetMenuName + nameof(ConsumableUpgrade))]
    public class ConsumableUpgrade : BaseUpgrade {
        [SerializeField, Min(1)] private int _maxCount;

        public int MaxCount => _maxCount;

        public override async Task<string> GetDescription() {
            Dictionary<string, string> arguments = new() {
                ["maxCount"] = _maxCount.ToString()
            };
            return await LocalizationManager.Instance.GetLocalization(_table, name + ".Description", arguments);
        }
    }
}
