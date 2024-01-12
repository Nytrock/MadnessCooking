using System;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;

    public Transform MainCamera => _mainCamera;
    public event Action<Transform> LocationChanged;

    public void ChangeLocation(Transform newLocation)
    {
        _mainCamera.position = newLocation.position;
        LocationChanged?.Invoke(_mainCamera);
    }
}
