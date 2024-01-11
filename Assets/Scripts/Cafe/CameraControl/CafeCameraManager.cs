using UnityEngine;


[RequireComponent(typeof(CafeManager))]
public class CafeCameraManager : CameraManager
{
    private CafeManager _cafeManager;
    protected override string _cameraAxis => "Mouse X";
    protected override string _keyAxis => "Horizontal";

    protected override void Start()
    {
        _cafeManager = GetComponent<CafeManager>();
        _cafeManager.SpaceAdded += CalculateBorderPositions;
        base.Start();
    }

    protected override void CalculateBorderPositions()
    {
        _startPosition = transform.position.x;
        var horzExtent = _mainCamera.orthographicSize * Screen.width / Screen.height;
        var spotSize = _cafeManager.GetSpaceSize();
        _endPosition = _startPosition + (_cafeManager.SpaceCount - 1) * spotSize + spotSize / 2 - horzExtent;
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
