using System;
using UnityEngine;

[Serializable]
public class BuyableItemRendererWithName : BuyableItemRenderer {
    [SerializeField] protected BuyableItemText _nameText;

    public override void SetItemInfo(BuyableItem item) {
        base.SetItemInfo(item);

        if (_nameText == null)
            return;

        _nameText.SetType(BuyableItemTextType.Name);
        _nameText.SetItem(item);
    }

    public override void ResetInfo() {
        base.ResetInfo();

        if (_nameText == null)
            return;

        _nameText.ResetItem();
    }
}
