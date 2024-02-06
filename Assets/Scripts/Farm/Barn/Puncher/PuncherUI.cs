using TMPro;
using UnityEngine;

public class PuncherUI : HoldAddUI
{
    [SerializeField] private TextMeshProUGUI _shitCount;

    public override void SetCountText(int countRaw, int countReady)
    {
        _shitCount.text = countRaw.ToString();
        base.SetCountText(countRaw, countReady);
    }
}
