using System;
using UnityEngine;

[Serializable]
public class ItemInfoRendererWithPrice : ItemInfoRendererWithDescription {
    [SerializeField] private LocalizedText _priceText;

    public void SetPrice(string buyNote, int price = -1) {
        if (price != -1)
            _priceText.AddArgumenst("price", price.ToString());
        _priceText.SetText(buyNote);
    }

    public override void ResetInfo() {
        base.ResetInfo();
        _priceText.SetText("");
    }
}
