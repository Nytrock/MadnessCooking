using System;
using TMPro;
using UnityEngine;

[Serializable]
public class ItemInfoRendererWithName : ItemInfoRenderer {
    [SerializeField] private TextMeshProUGUI _nameText;

    public override void SetItemInfo(BuyableItem item) {
        base.SetItemInfo(item);
        _nameText.text = item.Name;
    }

    public override void ResetInfo() {
        base.ResetInfo();
        _nameText.text = "";
    }
}
