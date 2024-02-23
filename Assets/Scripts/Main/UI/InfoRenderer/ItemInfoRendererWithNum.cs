using System;
using TMPro;
using UnityEngine;

[Serializable]
public class ItemInfoRendererWithNum : ItemInfoRenderer
{
    [SerializeField] private TextMeshProUGUI _num;

    public void SetNumText(string count)
    {
        _num.text = count;
    }

    public override void ResetInfo()
    {
        base.ResetInfo();
        _num.text = "";
    }
}
