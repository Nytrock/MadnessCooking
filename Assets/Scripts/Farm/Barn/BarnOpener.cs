using UnityEngine;

public class BarnOpener : MonoBehaviour
{
    [SerializeField] private Transform _amber;
    [SerializeField] private Transform _farm;
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private GameObject _mainUI;
    private bool _isOpen;

    public void ChangeBarnState()
    {
        _isOpen = !_isOpen;
        _mainUI.SetActive(!_isOpen);
        if (_isOpen)
            _locationManager.ChangeLocation(_amber);
        else
            _locationManager.ChangeLocation(_farm);
    }
}