using System;
using UnityEngine;

public class LocationActivator : ColliderActivator {
    [SerializeField] private Location _enterLocation;
    [SerializeField] private Location _leaveLocation;
    [SerializeField] private LocationManager _locationManager;
    private bool _isEnterLocation;

    public event Action<bool> StateChanged;

    protected void Awake() {
        _locationManager.LocationChanged += CheckLocation;
    }

    private void CheckLocation(Location location) {
        if (location == _enterLocation && !_isEnterLocation)
            _isEnterLocation = true;

        if (location == _leaveLocation && _isEnterLocation)
            _isEnterLocation = false;
    }

    protected override void Press() {
        ChangeLocation();
    }

    public void ChangeLocation() {
        _isEnterLocation = !_isEnterLocation;
        StateChanged?.Invoke(_isEnterLocation);

        if (_isEnterLocation)
            _locationManager.ChangeLocation(_enterLocation);
        else
            _locationManager.ChangeLocation(_leaveLocation);
    }
}
