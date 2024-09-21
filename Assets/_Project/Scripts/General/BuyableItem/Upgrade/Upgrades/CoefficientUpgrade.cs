using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(CoefficientUpgrade))]
public class CoefficientUpgrade : BaseUpgrade {
    [SerializeField, Min(0)] private float _coefficient;

    public float Coefficient => _coefficient;

    protected override string GetDescription() {
        Dictionary<string, string> arguments = new() {
            ["coefficient"] = _coefficient.ToString()
        };
        return LocalizationManager.Instance.GetLocalization(_table, name + ".Description", arguments);
    }
}
