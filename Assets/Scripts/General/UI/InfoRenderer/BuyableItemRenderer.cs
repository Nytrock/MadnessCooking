using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BuyableItemRenderer {
    [SerializeField] private Image _icon;

    public virtual void SetItemInfo(BuyableItem item) {
        _icon.sprite = item.Icon;
        _icon.color += new Color(0, 0, 0, 1);
    }

    public virtual void ResetInfo() {
        _icon.sprite = null;
        _icon.color *= new Color(1, 1, 1, 0);
    }
}
