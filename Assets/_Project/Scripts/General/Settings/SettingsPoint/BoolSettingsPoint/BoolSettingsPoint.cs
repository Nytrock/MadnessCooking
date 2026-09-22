using UnityEngine;
using UnityEngine.UI;

namespace MadnessCooking.General {
    public class BoolSettingsPoint : SettingsPoint<bool> {
        [SerializeField] private Toggle _toggle;

        public override void UpdateState() {
            base.UpdateState();
            _toggle.isOn = _data.LastValue;
        }

        protected void Awake() {
            _toggle.onValueChanged.AddListener(ChangeValue);
        }
    }
}
