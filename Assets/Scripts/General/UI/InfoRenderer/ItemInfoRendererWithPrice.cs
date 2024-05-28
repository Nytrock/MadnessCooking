using System;
using TMPro;
using UnityEngine;

[Serializable]
public class ItemInfoRendererWithPrice : ItemInfoRenderer {
    [SerializeField] private TextMeshProUGUI _priceText;

    public void SetPrice(string buyNote, int price = -1) {
        if (price == -1)
            _priceText.text = buyNote;
        else
            _priceText.text = buyNote + " " + price.ToString();
    }

    public override void ResetInfo() {
        base.ResetInfo();
        _priceText.text = "";
    }
}
