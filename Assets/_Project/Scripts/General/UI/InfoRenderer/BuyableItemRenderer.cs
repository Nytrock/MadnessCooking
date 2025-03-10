using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BuyableItemRenderer {
    [SerializeField] private Image _icon;

    public virtual void SetItemInfo(BuyableItem item) {
        SetIcon(item.Icon, Color.white);
    }

    public virtual void SetHiddenItemInfo(BuyableItem item) {
        SetIcon(item.Icon, Color.black);
    }

    public virtual void ResetInfo() {
        SetIcon(null, new Color(1, 1, 1, 0));
    }

    protected void SetIcon(Sprite sprite, Color color) {
        _icon.sprite = sprite;
        _icon.color = color;
    }
}
