using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(CoefficientFarmBedUpgrade))]
public class CoefficientFarmBedUpgrade : FarmBedUpgrade {
    [SerializeField, Min(0)] private float _coefficient;

    public float Coefficient => _coefficient;

    protected override string GetDescription() {
        Dictionary<string, string> arguments = new() {
            ["coefficient"] = _coefficient.ToString()
        };
        return LocalizationManager.Instance.GetLocalization(_table, name + ".Description", arguments);
    }
}
