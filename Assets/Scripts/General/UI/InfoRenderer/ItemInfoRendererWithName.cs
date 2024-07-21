using System;
using UnityEngine;

[Serializable]
public class ItemInfoRendererWithName : ItemInfoRenderer {
    [SerializeField] private LocalizedText _nameText;

    public override void SetItemInfo(BuyableItem item) {
        base.SetItemInfo(item);
        _nameText.SetText(item.Name);
    }

    public override void ResetInfo() {
        base.ResetInfo();
        _nameText.SetText("");
    }
}
