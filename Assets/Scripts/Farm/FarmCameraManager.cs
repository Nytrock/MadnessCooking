using UnityEngine;

public class FarmCameraManager : CameraManager<FarmData>
{
    protected override string _cameraAxis => "Mouse Y";
    protected override string _keyAxis => "Vertical";

    protected override void CalculateBorderPositions()
    {
        SpaceManagerData spaceData = _spaceManager.SpaceData;
        _endPosition = transform.position.y;
        _startPosition = _endPosition - (spaceData.Count - 1) * spaceData.SpaceSize;
        InvokeBordersFound();
    }

    protected override void MoveCamera()
    {
        float newPosition = _mainCameraPos.position.y - _cameraVelocity * Time.deltaTime * _cameraSpeed;
        newPosition = Mathf.Clamp(newPosition, _startPosition, _endPosition);
        SetCameraPosition(newPosition);
    }

    public override void SetCameraPosition(float newPosition)
    {
        _mainCameraPos.position = new Vector3(_mainCameraPos.position.x, newPosition, _mainCameraPos.position.z);
    }
}
