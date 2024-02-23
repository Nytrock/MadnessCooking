using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class ItemInfoRenderer
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;

    public void SetItemInfo(BuyableObject item)
    {
        _icon.sprite = item.Icon;
        _icon.color += new Color(0, 0, 0, 1);

        _name.text = item.Name;
        _description.text = item.Description;
    }

    public virtual void ResetInfo()
    {
        _icon.sprite = null;
        _icon.color *= new Color(1, 1, 1, 0);

        _name.text = "";
        _description.text = "";
    }
}
