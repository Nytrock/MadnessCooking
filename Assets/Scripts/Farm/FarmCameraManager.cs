using UnityEngine;

[RequireComponent(typeof(FarmManager))]
public class FarmCameraManager : CameraManager
{
    protected override string _cameraAxis => "Mouse Y";
    protected override string _keyAxis => "Vertical";

    protected override void CalculateBorderPositions()
    {
        FarmManager farmManager = GetComponent<FarmManager>();
        _endPosition = transform.position.y;
        _startPosition = _endPosition - (farmManager.BedsCount - 1) * farmManager.GetBedSize();
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
