using System;
using TMPro;
using UnityEngine;

[Serializable]
public class ItemInfoRendererWithPrice : ItemInfoRenderer
{
    [SerializeField] private TextMeshProUGUI _price;

    public void SetPrice(string buyText, int count = -1)
    {
        if (count == -1)
            _price.text = buyText;
        else
            _price.text = buyText + " " + count.ToString();
    }

    public override void ResetInfo()
    {
        base.ResetInfo();
        _price.text = "";
    }
}
