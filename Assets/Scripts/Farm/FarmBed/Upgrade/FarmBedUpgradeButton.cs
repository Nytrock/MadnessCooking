using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FarmBedUpgradeButton : ChoiceBuyButton<FarmBedUpgrade>
{
    [SerializeField] private TextMeshProUGUI _name;

    public override void Setup(FarmBedUpgrade item, int index, ChoiceBuyUI<FarmBedUpgrade> ui)
    {
        base.Setup(item, index, ui);
        _name.text = item.Name;
    }
}
