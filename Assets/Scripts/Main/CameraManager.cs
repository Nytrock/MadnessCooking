using System;
using Unity.Mathematics;
using UnityEngine;

public abstract class CameraManager : MonoBehaviour
{
    [SerializeField] protected Camera _mainCamera;
    [SerializeField] protected LocationManager _locationManager;
    protected float _startPosition;
    protected float _endPosition;
    protected abstract string _cameraAxis { get; }
    protected abstract string _keyAxis { get; }

    [SerializeField] protected float _cameraSpeed = 1;
    [SerializeField] protected float _keyVelocity = 1;
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
        BordersFound?.Invoke();
    }

    private void Update()
    {
        if (!_isWorking)
            return;

        var keyAxis = Input.GetAxis(_keyAxis);
        var cameraAxis = Input.GetAxis(_cameraAxis);

        if (keyAxis != 0) {
            _cameraVelocity = _keyVelocity * -Mathf.Sign(keyAxis);
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

    public virtual void SetCameraPosition(float newPosition) { }

    private void ChangeWorkMode(Transform newPosition)
    {
        _isWorking = newPosition.position.x == transform.position.x;
    }

    public void ChangeWorkMode(bool newState)
    {
        _isWorking = newState;
        _cameraVelocity = 0;
    }

    protected virtual void CalculateBorderPositions() { }

    protected  virtual void MoveCamera() { }
}
