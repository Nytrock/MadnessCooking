using UnityEngine;


public class HorizontalCameraManager : CameraManager {
    private float _horizontalExtention;

    protected override string _cameraAxis => "Mouse X";
    protected override string _keyAxis => "Horizontal";

    protected override void Awake() {
        base.Awake();
        _horizontalExtention = _mainCamera.orthographicSize * Screen.width / Screen.height;
    }

    protected override void CalculateBorderPositions() {
        _startPosition = transform.position.x;
        _endPosition = _startPosition + _spaceManager.GetSpacesSize()
            + _spaceManager.SpaceSize - _horizontalExtention;
        InvokeBordersFound();
    }

    protected override void MoveCamera() {
        float newPosition = _cameraTransform.position.x - (_cameraVelocity * Time.deltaTime * _cameraSpeed);
        newPosition = Mathf.Clamp(newPosition, _startPosition, _endPosition);
        SetCameraPosition(newPosition);
    }

    public override void SetCameraPosition(float newPosition) {
        Vector3 cameraPosition = new(newPosition, _cameraTransform.position.y, _cameraTransform.position.z);
        _data.UpdateCameraPosition(cameraPosition);
        _cameraTransform.position = cameraPosition;
    }
}
