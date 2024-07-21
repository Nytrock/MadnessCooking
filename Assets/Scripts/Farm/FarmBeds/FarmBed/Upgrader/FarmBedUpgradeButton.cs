using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FarmBedUpgradeButton : ChoiceBuyButton<FarmBedUpgrade> {
    [SerializeField] private LocalizedText _nameText;

    public override void Setup(FarmBedUpgrade item, int index, ChoiceBuyUI<FarmBedUpgrade> ui) {
        base.Setup(item, index, ui);
        _nameText.SetText(item.Name);
    }
}
