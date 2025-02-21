using System;
using UnityEngine;

public abstract class CameraManager : MonoBehaviour {
    [SerializeField] protected Camera _mainCamera;
    [SerializeField] protected LocationManager _locationManager;
    [SerializeField] protected UIHoverListener _hoverListener;
    [SerializeField] protected Location _location;
    [SerializeField] protected LocationSlider _locationSlider;
    [SerializeField] protected SpaceManager _spaceManager;
    [SerializeField] protected TutorialManager _tutorialManager;
    protected float _startPosition;
    protected float _endPosition;
    protected abstract string _mouseAxis { get; }
    protected abstract string _keyAxis { get; }

    [SerializeField] protected float _cameraSpeed = 1;
    [SerializeField] protected float _keyVelocity = 1;
    [SerializeField] protected float _scrollVelocity = 1;
    [SerializeField] protected float _mouseVelocity = 1;
    [SerializeField] protected float _speedFading = 1;

    protected Transform _cameraTransform;
    protected float _cameraVelocity;
    protected bool _isWorking = true;
    private bool _isKeyPressed = true;
    protected CameraManagerData _data;

    public event Action CameraMoved;
    public event Action BordersFound;

    public float StartPosition => _startPosition;
    public float EndPosition => _endPosition;
    public Transform CameraTransform => _cameraTransform;
    public bool IsMouseMoving => Mathf.Abs(_cameraVelocity) >= 0.01f && !_isKeyPressed;

    protected virtual void Awake() {
        _locationManager.LocationChanged += ChangeWorkMode;
        _spaceManager.SpaceAdded += CalculateBorderPositions;
        _cameraTransform = _mainCamera.transform;
        _locationSlider.Bootup(this);
    }

    private void Update() {
        if (!_isWorking || _tutorialManager.IsWork)
            return;

        float keyAxis = 0, scrollAxis = 0, mouseAxis = 0;
        if (!_hoverListener.IsHover) {
            keyAxis = Input.GetAxis(_keyAxis) / FpsManager.NORMALIZED_DELTA_TIME;
            mouseAxis = Input.GetAxis(_mouseAxis) / FpsManager.NORMALIZED_DELTA_TIME;
        }

        if (!_hoverListener.IsScrollBlocked)
            scrollAxis = Input.GetAxis("Mouse ScrollWheel") / FpsManager.NORMALIZED_DELTA_TIME;

        if (keyAxis != 0 || scrollAxis != 0) {
            if (keyAxis != 0)
                _cameraVelocity = _keyVelocity * -Mathf.Sign(keyAxis);
            else
                _cameraVelocity = _scrollVelocity * -Mathf.Sign(scrollAxis);
            _isKeyPressed = true;
        } else if (_isKeyPressed) {
            _cameraVelocity = 0;
            _isKeyPressed = false;
        } else if (Input.GetMouseButton(0)) {
            _cameraVelocity = _mouseVelocity * mouseAxis;
        } else {
            float fadingVelocity = Time.deltaTime * _speedFading;
            if (Mathf.Abs(_cameraVelocity) <= fadingVelocity)
                _cameraVelocity = 0;
            else
                _cameraVelocity -= Mathf.Sign(_cameraVelocity) * fadingVelocity;
        }

        if (Mathf.Abs(_cameraVelocity) >= 0.01f) {
            InvokeCameraMoved();
            MoveCamera();
        }
    }

    private void ChangeWorkMode(Location newLocation) {
        _isWorking = newLocation == _location;
        if (_isWorking)
            _cameraTransform.position = _data.CameraPosition;
        InvokeCameraMoved();
    }

    public void ChangeWorkMode(bool newState) {
        _isWorking = newState;
        _cameraVelocity = 0;
    }

    protected void InvokeBordersFound() {
        BordersFound?.Invoke();
    }

    protected void InvokeCameraMoved() {
        CameraMoved?.Invoke();
    }

    public abstract void SetCameraPosition(float newPosition);

    protected abstract void CalculateBorderPositions();

    protected abstract void MoveCamera();
}
