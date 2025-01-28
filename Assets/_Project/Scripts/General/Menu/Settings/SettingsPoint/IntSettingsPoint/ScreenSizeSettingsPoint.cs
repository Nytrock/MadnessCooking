using TMPro;
using UnityEngine;

public class ScreenSizeSettingsPoint : IntSettingsPoint {
    [SerializeField] private TextMeshProUGUI _text;

    protected override void UpdateState() {
        base.UpdateState();
        ScreenSize nowScreenSize = (_settingable.Value as ScreenSizeManager).GetNowScreenSize();
        _text.text = nowScreenSize.ToString();
    }
}
