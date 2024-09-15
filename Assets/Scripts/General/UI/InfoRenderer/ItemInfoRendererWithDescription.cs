using System;
using UnityEngine;

[Serializable]
public class ItemInfoRendererWithDescription : BuyableItemRendererWithName {
    [SerializeField] private BuyableItemText _descriptionText;

    public override void SetItemInfo(BuyableItem item) {
        base.SetItemInfo(item);

        if (_descriptionText == null)
            return;

        _descriptionText.SetType(BuyableItemTextType.Desctiption);
        _descriptionText.SetItem(item);
    }

    public override void ResetInfo() {
        base.ResetInfo();

        if (_descriptionText == null)
            return;

        _descriptionText.ResetItem();
    }
}
