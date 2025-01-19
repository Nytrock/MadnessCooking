using UnityEngine;

public class VerticalCameraManager : CameraManager {
    protected override string _cameraAxis => "Mouse Y";
    protected override string _keyAxis => "Vertical";

    protected override void CalculateBorderPositions() {
        _endPosition = transform.position.y;
        _startPosition = _endPosition - _spaceManager.GetSpacesSize();
        InvokeBordersFound();
    }

    protected override void MoveCamera() {
        float newPosition = _cameraTransform.position.y - (_cameraVelocity * Time.deltaTime * _cameraSpeed);
        newPosition = Mathf.Clamp(newPosition, _startPosition, _endPosition);
        SetCameraPosition(newPosition);
    }

    public override void SetCameraPosition(float newPosition) {
        Vector3 cameraPosition = new(_cameraTransform.position.x, newPosition, _cameraTransform.position.z);
        _data.UpdateCameraPosition(cameraPosition);
        _cameraTransform.position = cameraPosition;
    }
}
