using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class ItemInfoRenderer
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _desctription;

    public void SetItemInfo(BuyableObject item)
    {
        _icon.sprite = item.Icon;
        _icon.color += new Color(0, 0, 0, 1);
        _name.text = item.Name;
        _desctription.text = item.Description;
    }

    public void SetCountText(string count)
    {
        _count.text = count;
    }

    public void ResetInfo()
    {
        _icon.sprite = null;
        _icon.color *= new Color(1, 1, 1, 0);

        _name.text = "";
        _desctription.text = "";
        if (_count != null)
           _count.text = "";
    }
}
