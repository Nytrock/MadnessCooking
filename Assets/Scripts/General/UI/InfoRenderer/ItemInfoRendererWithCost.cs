using System;
using TMPro;
using UnityEngine;

[Serializable]
public class ItemInfoRendererWithCost : ItemInfoRenderer
{
    [SerializeField] private TextMeshProUGUI _costText;

    public void SetCost(string buyNote, int cost = -1)
    {
        if (cost == -1)
            _costText.text = buyNote;
        else
            _costText.text = buyNote + " " + cost.ToString();
    }

    public override void ResetInfo()
    {
        base.ResetInfo();
        _costText.text = "";
    }
}
