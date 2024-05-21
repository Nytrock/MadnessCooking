using UnityEngine;


public class CafeCameraManager : CameraManager<CafeData>
{
    private float _horizontalExtention;

    protected override string _cameraAxis => "Mouse X";
    protected override string _keyAxis => "Horizontal";

    protected override void Awake()
    {
        base.Awake();
        _horizontalExtention = _mainCamera.orthographicSize * Screen.width / Screen.height;
    }

    protected override void CalculateBorderPositions()
    {
        var spaceData = _spaceManager.SpaceData;
        _startPosition = transform.position.x;
        _endPosition = _startPosition + (spaceData.Count - 1) * spaceData.SpaceSize 
            + spaceData.SpaceSize / 2 - _horizontalExtention;
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
