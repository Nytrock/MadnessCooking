using System;
using UnityEngine;

public abstract class CameraManager : MonoBehaviour
{
    [SerializeField] protected Camera _mainCamera;
    [SerializeField] protected LocationManager _locationManager;
    [SerializeField] protected float _startPosition;
    [SerializeField] protected float _endPosition;
    protected abstract string _cameraAxis { get; }
    protected abstract string _keyAxis { get; }

    [SerializeField] protected float _cameraSpeed = 1;
    [SerializeField] protected float _keyVelocity = 1;
    [SerializeField] protected float _mouseVelocity = 1;
    [SerializeField] protected float _speedFading = 1;
    protected Transform _mainCameraPos;
    protected float _cameraVelocity;
    protected bool _isWorking = true;
    private bool _isKeyPressed = true;

    public event Action CameraMoved;
    public event Action BordersFound;

    public float StartPosition => _startPosition;
    public float EndPosition => _endPosition;
    public Transform MainCameraPos => _mainCameraPos;


    protected virtual void Start()
    {
        _locationManager.LocationChanged += ChangeWorkMode;
        _mainCameraPos = _mainCamera.transform;
        ChangeWorkMode(_mainCameraPos);

        CalculateBorderPositions();
    }

    private void Update()
    {
        if (!_isWorking)
            return;

        var keyAxis = Input.GetAxis(_keyAxis);
        var mouseAxis = Input.GetAxis("Mouse ScrollWheel");
        var cameraAxis = Input.GetAxis(_cameraAxis);

        if (keyAxis != 0 || mouseAxis != 0) {
            if (keyAxis != 0)
                _cameraVelocity = _keyVelocity * -Mathf.Sign(keyAxis);
            else
                _cameraVelocity = _mouseVelocity * -Mathf.Sign(mouseAxis);
            _isKeyPressed = true;
        } else if (_isKeyPressed) {
            _cameraVelocity = 0;
            _isKeyPressed = false;
        } else if (Input.GetMouseButton(0)) {
            _cameraVelocity = cameraAxis;
        } else if (Mathf.Abs(_cameraVelocity) >= 0.01) {
            _cameraVelocity -= Time.deltaTime * Mathf.Sign(_cameraVelocity) * _speedFading;
        }

        if (Mathf.Abs(_cameraVelocity) >= 0.01) {
            CameraMoved?.Invoke();
            MoveCamera();
        }
    }

    private void ChangeWorkMode(Transform newPosition)
    {
        _isWorking = newPosition.position.x == transform.position.x;
        CameraMoved?.Invoke();
    }

    public void ChangeWorkMode(bool newState)
    {
        _isWorking = newState;
        _cameraVelocity = 0;
    }

    protected void InvokeBordersFound()
    {
        BordersFound?.Invoke();
    }

    public abstract void SetCameraPosition(float newPosition);

    protected abstract void CalculateBorderPositions();

    protected abstract void MoveCamera();
}
