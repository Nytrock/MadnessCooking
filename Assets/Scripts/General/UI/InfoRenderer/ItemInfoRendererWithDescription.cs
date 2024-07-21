using System;
using UnityEngine;

[Serializable]
public class ItemInfoRendererWithDescription : ItemInfoRendererWithName {
    [SerializeField] private LocalizedText _descriptionText;

    public override void SetItemInfo(BuyableItem item) {
        base.SetItemInfo(item);
        _descriptionText.SetText(item.Description);
    }

    public override void ResetInfo() {
        base.ResetInfo();
        _descriptionText.SetText("");
    }
}
