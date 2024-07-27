using UnityEngine;

public class LocationAdditionUI : MonoBehaviour {
    [SerializeField] private Location _location;
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private GameObject _UI;

    private void Awake() {
        _locationManager.LocationChanged += UpdateUI;
    }

    private void UpdateUI(Location newLocation) {
        _UI.SetActive(_location == newLocation);
    }

    public void ChangeUIState(bool newValue) {
        _UI.SetActive(newValue);
    }
}
