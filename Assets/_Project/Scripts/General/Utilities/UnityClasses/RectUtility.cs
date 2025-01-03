using UnityEngine;

public static class RectUtility {
    public static bool ContainsCamera(this RectTransform rect) {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect,
            Input.mousePosition, Camera.main, out Vector2 localMousePosition);
        return rect.rect.Contains(localMousePosition);
    }
}
