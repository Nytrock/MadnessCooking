using System;
using TMPro;
using UnityEngine;

[Serializable]
public class ItemInfoRendererWithDescription : ItemInfoRenderer {
    [SerializeField] private TextMeshProUGUI _descriptionText;

    public override void SetItemInfo(BuyableItem item) {
        base.SetItemInfo(item);
        _descriptionText.text = item.Description;
    }

    public override void ResetInfo() {
        base.ResetInfo();
        _descriptionText.text = "";
    }
}
