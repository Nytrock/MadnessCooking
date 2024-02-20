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
        _name.text = item.Name;
        _desctription.text = item.Description;
    }

    public void SetCount(string count)
    {
        _count.text = count;
    }
}
