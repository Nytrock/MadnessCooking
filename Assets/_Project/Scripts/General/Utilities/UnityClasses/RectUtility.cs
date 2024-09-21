using UnityEngine;

public static class RectUtility {
    public static bool ContainsCamera(this RectTransform rect) {
        Vector2 localMousePosition = rect.InverseTransformPoint(Input.mousePosition);
        return rect.rect.Contains(localMousePosition);
    }
}
