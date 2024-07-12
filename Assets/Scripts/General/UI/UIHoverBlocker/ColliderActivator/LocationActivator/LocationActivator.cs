using UnityEngine;

public class LocationActivator : ColliderActivator {
    [SerializeField] private Transform _enterTarget;
    [SerializeField] private Transform _leaveTarget;
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private GameObject _mainUI;
    [SerializeField, Min(0)] private float _fatigueCoef;
    private bool _isOpen;

    protected override void Press() {
        ChangeLocation();
    }

    public void ChangeLocation() {
        _isOpen = !_isOpen;
        _mainUI.SetActive(!_isOpen);
        if (_isOpen) {
            _locationManager.ChangeLocation(_enterTarget.position);
            FatigueManager.Instance.ChangeFatigue(_fatigueCoef);
        } else {
            _locationManager.ChangeLocation(_leaveTarget.position);
        }
    }
}
