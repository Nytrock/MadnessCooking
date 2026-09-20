using UnityEngine.Events;
using UnityEngine.UI;

namespace MadnessCooking.General {
    public static class ButtonUtility {
        public static void OverrideAllListeners(this Button button, UnityAction action) {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
        }
    }
}
