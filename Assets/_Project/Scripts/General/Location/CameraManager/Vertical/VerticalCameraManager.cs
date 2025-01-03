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
        _cameraPosition = new Vector3(_cameraTransform.position.x, newPosition, _cameraTransform.position.z);
        _cameraTransform.position = _cameraPosition;
    }
}
