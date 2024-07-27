using UnityEngine;

public class LocationActivator : ColliderActivator {
    [SerializeField] private Location _enterLocation;
    [SerializeField] private Location _leaveLocation;
    [SerializeField] private LocationManager _locationManager;
    private bool _isOpen;

    protected override void Press() {
        ChangeLocation();
    }

    public void ChangeLocation() {
        _isOpen = !_isOpen;
        if (_isOpen)
            _locationManager.ChangeLocation(_enterLocation);
        else
            _locationManager.ChangeLocation(_leaveLocation);
    }
}
