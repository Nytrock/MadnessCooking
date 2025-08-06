using UnityEngine;

public class NonMobileObject : MonoBehaviour {
    private void Awake() {
        if (!Application.isMobilePlatform)
            return;

        gameObject.SetActive(false);
    }
}
