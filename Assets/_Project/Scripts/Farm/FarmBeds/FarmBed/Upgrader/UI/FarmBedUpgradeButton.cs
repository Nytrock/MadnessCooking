using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BedTypeStyleUpdater))]
public class FarmBedUpgradeButton : ChoiceBuyButton<FarmBedUpgrade> {
    [SerializeField] private LocalizedText _nameText;
    private BedTypeStyleUpdater _renderer;

    public override void Setup(FarmBedUpgrade item, int index, ChoiceBuyUI<FarmBedUpgrade> ui) {
        _button = GetComponent<Button>();
        gameObject.SetActive(true);

        Item = item;
        _price = item.PriceToAdd;
        _icon.sprite = Item.Icon;
        CheckBuyable(MoneyManager.Instance.MoneyCount);
        _button.onClick.AddListener(
            delegate { ui.Choice(index, _isBuyable); }
        );
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
