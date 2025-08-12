using UnityEngine;

public static class PlatformManager {
    public static bool IsMobile => Application.isMobilePlatform;
    public static bool IsWeb => Application.platform == RuntimePlatform.WebGLPlayer;
    public static bool IsNotDesktop => IsMobile || IsWeb;
}
