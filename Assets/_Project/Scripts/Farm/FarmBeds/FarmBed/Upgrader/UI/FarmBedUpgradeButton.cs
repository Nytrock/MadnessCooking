using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BedTypeStyleUpdater))]
public class FarmBedUpgradeButton : ChoiceBuyButton<FarmBedUpgrade> {
    [SerializeField] private LocalizedText _nameText;
    private BedTypeStyleUpdater _renderer;

    public override void Setup(FarmBedUpgrade item, UnityAction buttonEvent) {
        base.Setup(item, buttonEvent);
        _price = item.PriceToAdd;
        CheckBuyable(MoneyManager.Instance.MoneyCount);

        _nameText.SetText(item.RawName);
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
