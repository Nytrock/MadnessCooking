using System;
using TMPro;
using UnityEngine;

[Serializable]
public class ItemInfoRendererWithPrice : ItemInfoRenderer
{
    [SerializeField] private TextMeshProUGUI _priceText;

    public void SetPrice(string buyText, int count = -1)
    {
        if (count == -1)
            _priceText.text = buyText;
        else
            _priceText.text = buyText + " " + count.ToString();
    }

    public override void ResetInfo()
    {
        base.ResetInfo();
        _priceText.text = "";
    }
}
