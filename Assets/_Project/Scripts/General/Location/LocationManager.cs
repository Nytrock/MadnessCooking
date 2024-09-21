using System;
using UnityEngine;

public class LocationManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LocationPoint[] _locations;
    [SerializeField] private GameObject _generalUI;
    private LocationManagerData _data;

    public event Action<Location> LocationChanged;

    private void LateStart() {
        ChangeLocation(_data.Location);
    }

    public void ChangeLocation(Location location) {
        LocationPoint locationPoint = null;
        foreach (var point in _locations)
            if (point.Location == location)
                locationPoint = point;
        if (locationPoint == null)
            throw new ArgumentNullException($"There is no point for {location} location");

        _data.ChangeLocation(locationPoint.Location);
        _mainCamera.transform.position = new Vector3(locationPoint.Point.x, locationPoint.Point.y, -10);
        _generalUI.SetActive(!locationPoint.IsHideUI);
        FatigueManager.Instance.ChangeFatigue(locationPoint.FatigueCoef);

        LocationChanged?.Invoke(locationPoint.Location);
    }

    public void Bind(GeneralData data) {
        data.LocationManager ??= new();
        _data = data.LocationManager;
        LateStart();
    }
}
