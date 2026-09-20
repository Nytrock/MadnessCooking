using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class ItemInfoRendererWithPrice : BuyableItemRendererWithDescription {
        [SerializeField] private LocalizedText _priceText;

        public void SetPrice(string buyNote, int price = -1) {
            if (price != -1)
                _priceText.AddArguments("price", price.ToString());
            _priceText.SetText(buyNote);
        }

        public override void ResetInfo() {
            base.ResetInfo();
            _priceText.ClearArguments();
            _priceText.SetText("General.Empty");
        }
    }
}
