using TMPro;
using UnityEngine;

namespace MadnessCooking.General {
    public class ScreenSizeSettingsPoint : IntSettingsPoint {
        [SerializeField] private TextMeshProUGUI _text;

        public override void UpdateState() {
            base.UpdateState();
            ScreenSize nowScreenSize = (_settingable as ScreenSizeManager).NowScreenSize;
            _text.text = nowScreenSize.ToString();
        }
    }
}
