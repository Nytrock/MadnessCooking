using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemInfoRenderer {
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    public virtual void SetItemInfo(BuyableItem item) {
        _icon.sprite = item.Icon;
        _icon.color += new Color(0, 0, 0, 1);

        _nameText.text = item.Name;
        _descriptionText.text = item.Description;
    }

    public virtual void ResetInfo() {
        _icon.sprite = null;
        _icon.color *= new Color(1, 1, 1, 0);

        _nameText.text = "";
        _descriptionText.text = "";
    }
}
