using TMPro;
using UnityEngine;

public class HoldDoubleAddUI : HoldAddUI
{
    [SerializeField] protected TextMeshProUGUI _materialCount;

    public override void SetCountText(int countRaw, int countReady)
    {
        _materialCount.text = countRaw.ToString();
        base.SetCountText(countRaw, countReady);
    }
}
