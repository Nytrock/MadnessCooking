using UnityEngine;

namespace MadnessCooking.General {
    public class ScreenModeSettingsPoint : IntSettingsPoint {
        [SerializeField] private LocalizedText _text;
        [SerializeField] private string _textExtension;

        public override void UpdateState() {
            base.UpdateState();
            ScreenMode nowScreenMode = (_settingable as ScreenModeManager).GetNowScreenMode();

            string modeName = _textExtension + nowScreenMode.ToString();
            _text.SetText(modeName);
        }
    }
}
