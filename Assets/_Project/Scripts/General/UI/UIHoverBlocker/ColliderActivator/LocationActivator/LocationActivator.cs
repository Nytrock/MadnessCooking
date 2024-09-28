using System;
using UnityEngine;

public class LocationActivator : ColliderActivator {
    [SerializeField] private Location _enterLocation;
    [SerializeField] private Location _leaveLocation;
    [SerializeField] private LocationManager _locationManager;
    private bool _isOpen;

    public event Action<bool> StateChanged;

    protected override void Press() {
        ChangeLocation();
    }

    public void ChangeLocation() {
        _isOpen = !_isOpen;
        StateChanged?.Invoke(_isOpen);

        if (_isOpen)
            _locationManager.ChangeLocation(_enterLocation);
        else
            _locationManager.ChangeLocation(_leaveLocation);
    }
}
