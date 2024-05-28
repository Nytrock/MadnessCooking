using System;
using UnityEngine;

public abstract class CameraManager<TData> : MonoBehaviour where TData : ISaveable {
    [SerializeField] protected Camera _mainCamera;
    [SerializeField] protected LocationManager _locationManager;
    [SerializeField] protected LocationSlider<TData> _locationSlider;
    [SerializeField] protected SpaceManager<TData> _spaceManager;
    protected float _startPosition;
    protected float _endPosition;
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

    protected virtual void Awake() {
        _locationManager.LocationChanged += ChangeWorkMode;
        _spaceManager.SpaceAdded += CalculateBorderPositions;
        _mainCameraPos = _mainCamera.transform;
        _locationSlider.Bootup(this);
    }

    private void Update() {
        if (!_isWorking)
            return;

        float keyAxis = Input.GetAxis(_keyAxis);
        float mouseAxis = Input.GetAxis("Mouse ScrollWheel");
        float cameraAxis = Input.GetAxis(_cameraAxis);

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

    private void ChangeWorkMode(Vector2 newPosition) {
        _isWorking = newPosition.x == transform.position.x;
        CameraMoved?.Invoke();
    }

    public void ChangeWorkMode(bool newState) {
        _isWorking = newState;
        _cameraVelocity = 0;
    }

    protected void InvokeBordersFound() {
        BordersFound?.Invoke();
    }

    public abstract void SetCameraPosition(float newPosition);

    protected abstract void CalculateBorderPositions();

    protected abstract void MoveCamera();
}
