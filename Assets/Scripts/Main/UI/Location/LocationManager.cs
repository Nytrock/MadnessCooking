using System;
using UnityEditor;
using UnityEngine;

public class LocationManager : MonoBehaviour, IBindable<MainData>
{
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private LocationButton[] _locations;
    private MainData _data;

    public event Action<Vector2> LocationChanged;

    private void LateStart()
    {
        ChangeLocation(_locations[_data.StartLocationId]);
    }

    public void ChangeLocation(Vector2 newLocation)
    {
        _mainCamera.position = new Vector3(newLocation.x, newLocation.y, -10);
        LocationChanged?.Invoke(newLocation);
    }

    public void ChangeLocation(LocationButton newLocation)
    {
        _data.StartLocationId = ArrayUtility.IndexOf(_locations, newLocation);
        ChangeLocation(newLocation.Location);
    }

    public void Bind(MainData data, bool isFileEmpty)
    {
        _data = data;
        LateStart();
    }
}
