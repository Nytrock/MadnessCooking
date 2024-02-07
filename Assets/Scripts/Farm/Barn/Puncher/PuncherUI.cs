using TMPro;
using UnityEngine;

public class PuncherUI : HoldDoubleAddUI
{
    public override void SetCountText(int countRaw, int countReady)
    {
        _materialCount.text = countRaw.ToString();
        base.SetCountText(countRaw, countReady);
    }
}
