using UnityEngine;

public class LocalHoverTextPanel : HoverTextPanel {

    [SerializeField] private Camera _camera;

    protected override void UpdatePosition() {
        Vector3 newPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        newPosition -= new Vector3(0, 0, newPosition.z);
        transform.position = newPosition;
    }
}
