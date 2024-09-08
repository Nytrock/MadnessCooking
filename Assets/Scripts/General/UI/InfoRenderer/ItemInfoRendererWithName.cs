using System;
using UnityEngine;

[Serializable]
public class ItemInfoRendererWithName : ItemInfoRenderer {
    [SerializeField] private LocalizedText _nameText;

    public override void SetItemInfo(BuyableItem item) {
        base.SetItemInfo(item);
        if (_nameText == null)
            return;

        _nameText.SetText(item.Name);
    }

    public override void ResetInfo() {
        base.ResetInfo();
        if (_nameText == null)
            return;

        _nameText.SetText("");
    }
}
