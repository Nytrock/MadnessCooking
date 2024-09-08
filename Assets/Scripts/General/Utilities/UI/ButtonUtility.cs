using UnityEngine.Events;
using UnityEngine.UI;

public static class ButtonUtility {
    public static void OverrideAllListeners(this Button button, UnityAction action) {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}
