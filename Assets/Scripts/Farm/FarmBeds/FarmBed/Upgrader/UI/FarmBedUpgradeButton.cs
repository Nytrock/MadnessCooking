using UnityEngine;

[RequireComponent(typeof(BedTypeStyleUpdater))]
public class FarmBedUpgradeButton : ChoiceBuyButton<FarmBedUpgrade> {
    [SerializeField] private LocalizedText _nameText;
    private BedTypeStyleUpdater _renderer;

    public override void Setup(FarmBedUpgrade item, int index, ChoiceBuyUI<FarmBedUpgrade> ui) {
        base.Setup(item, index, ui);
        _nameText.SetText(item.Name);
    }

    public void UpdateStyle(BedType bedType) {
        if (_renderer == null)
            GetStyleChanger();

        _renderer.UpdateStyle(bedType);
    }

    private void GetStyleChanger() {
        _renderer = GetComponent<BedTypeStyleUpdater>();
    }
}
