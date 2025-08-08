using UnityEngine;

public class PlatformSpecificObject : MonoBehaviour {
    [SerializeField] private bool _forDesktop;
    [SerializeField] private bool _forMobile;
    [SerializeField] private bool _forWeb;

    private void Awake() {
        gameObject.SetActive(GetState());
    }

    private bool GetState() {
        bool isMobile = Application.isMobilePlatform;
        bool isWeb = Application.platform == RuntimePlatform.WebGLPlayer;

        if (!_forMobile && isMobile)
            return false;

        if (!_forWeb && isWeb)
            return false;

        if (!_forDesktop && !isMobile && !isWeb)
            return false;

        return true;
    }
}
