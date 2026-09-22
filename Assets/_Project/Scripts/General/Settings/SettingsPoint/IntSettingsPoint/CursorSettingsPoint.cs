using UnityEngine;
using UnityEngine.UI;

namespace MadnessCooking.General {
    public class CursorSettingsPoint : IntSettingsPoint {
        [SerializeField] private Image _cursorImage;

        public override void UpdateState() {
            base.UpdateState();
            _cursorImage.sprite = (_settingable as CursorManager).GetNowCursor();
            _cursorImage.SetNativeSize();
        }
    }
}
