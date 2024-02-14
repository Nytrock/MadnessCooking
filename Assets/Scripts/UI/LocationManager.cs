using System;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;

    public Transform MainCamera => _mainCamera;
    public event Action<Transform> LocationChanged;

    public void ChangeLocation(Transform newLocation)
    {
        _mainCamera.position = new Vector3(newLocation.position.x, newLocation.position.y, -10);
        LocationChanged?.Invoke(_mainCamera);
    }
}
