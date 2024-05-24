using System;
using UnityEditor;
using UnityEngine;

public class LocationManager : MonoBehaviour, IBindable<GeneralData>
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LocationButton[] _locations;
    private GeneralData _data;

    public event Action<Vector2> LocationChanged;

    private void LateStart()
    {
        ChangeLocation(_locations[_data.StartLocationId]);
    }

    public void ChangeLocation(Vector2 newLocation)
    {
        _mainCamera.transform.position = new Vector3(newLocation.x, newLocation.y, -10);
        LocationChanged?.Invoke(newLocation);
    }

    public void ChangeLocation(LocationButton newLocation)
    {
        _data.StartLocationId = ArrayUtility.IndexOf(_locations, newLocation);
        ChangeLocation(newLocation.Location);
    }

    public void Bind(GeneralData data, bool isFileEmpty)
    {
        _data = data;
        LateStart();
    }
}
