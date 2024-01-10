using UnityEngine;


[RequireComponent(typeof(CafeManager))]
public class CafeCameraManager : CameraManager
{
    protected override string _cameraAxis => "Mouse X";
    protected override string _keyAxis => "Horizontal";

    protected override void CalculateBorderPositions()
    {
        CafeManager cafeManager = GetComponent<CafeManager>();
        _startPosition = transform.position.x;
        var horzExtent = _mainCamera.orthographicSize * Screen.width / Screen.height;
        var spotSize = cafeManager.GetSpotSize();
        _endPosition = _startPosition + (cafeManager.SpotCount - 1) * spotSize + spotSize / 2 - horzExtent;
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
