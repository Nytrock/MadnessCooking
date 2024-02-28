using System;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private LocationButton _defaultPosition;

    public Transform MainCamera => _mainCamera;
    public event Action<Transform> LocationChanged;

    private void Start()
    {
        ChangeLocation(_defaultPosition.Location);
    }

    public void ChangeLocation(Transform newLocation)
    {
        _mainCamera.position = new Vector3(newLocation.position.x, newLocation.position.y, -10);
        LocationChanged?.Invoke(_mainCamera);
    }
}
