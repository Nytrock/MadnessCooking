using UnityEngine;
using UnityEngine.UI;

public class CursorSettingsPoint : IntSettingsPoint {
    [SerializeField] private Image _cursorImage;

    protected override void UpdateState() {
        base.UpdateState();
        _cursorImage.sprite = (_settingable.Value as CursorManager).GetNowCursor();
        _cursorImage.SetNativeSize();
    }
}
