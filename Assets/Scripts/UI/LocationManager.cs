using System;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;

    public event Action<Transform> LocationChanged;

    private void Start()
    {
        LocationChanged?.Invoke(_mainCamera);
    }

    public void ChangeLocation(Transform newLocation)
    {
        _mainCamera.position = newLocation.position;
        LocationChanged.Invoke(_mainCamera);
    }
}
