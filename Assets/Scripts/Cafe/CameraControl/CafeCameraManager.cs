using UnityEngine;


public class CafeCameraManager : CameraManager
{
    private float _horizontalExtention;

    protected override string _cameraAxis => "Mouse X";
    protected override string _keyAxis => "Horizontal";

    protected override void Start()
    {
        _horizontalExtention = _mainCamera.orthographicSize * Screen.width / Screen.height;
        base.Start();
    }

    protected override void CalculateBorderPositions()
    {
        _startPosition = transform.position.x;
        var spaceSize = _spaceManager.GetSpaceSize();
        _endPosition = _startPosition + (_spaceManager.SpaceCount - 1) * spaceSize + spaceSize / 2 - _horizontalExtention;
        InvokeBordersFound();
    }

    protected override void MoveCamera()
    {
        float newPosition = _mainCameraPos.position.x - _cameraVelocity * Time.deltaTime * _cameraSpeed;
        newPosition = Mathf.Clamp(newPosition, _startPosition, _endPosition);
        SetCameraPosition(newPosition);
    }

    public override void SetCameraPosition(float newPosition)
    {
        _mainCameraPos.position = new Vector3(newPosition, _mainCameraPos.position.y, _mainCameraPos.position.z);
    }
}
