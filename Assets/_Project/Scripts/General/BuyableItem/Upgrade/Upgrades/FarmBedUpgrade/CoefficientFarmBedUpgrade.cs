using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MadnessCooking.General {
    [CreateAssetMenu(menuName = AssetMenuName + nameof(CoefficientFarmBedUpgrade))]
    public class CoefficientFarmBedUpgrade : FarmBedUpgrade {
        [SerializeField, Min(0)] private float _coefficient;

        public float Coefficient => _coefficient;

        public override async Task<string> GetDescription() {
            Dictionary<string, string> arguments = new() {
                ["coefficient"] = _coefficient.ToString()
            };
            return await LocalizationManager.Instance.GetLocalization(_table, name + ".Description", arguments);
        }
    }
}
