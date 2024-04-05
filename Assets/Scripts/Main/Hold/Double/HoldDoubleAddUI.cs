using TMPro;
using UnityEngine;

public class HoldDoubleAddUI : HoldAddUI
{
    [SerializeField] protected CountRenderer _materialCount;

    public override void SetCountText(int countRaw, int countReady)
    {
        _materialCount.UpdateCount(countRaw);
        base.SetCountText(countRaw, countReady);
    }
}
