using UnityEngine;
using UnityEngine.UI;

namespace MadnessCooking.General {
    [RequireComponent(typeof(Button))]
    public class LocationButton : MonoBehaviour {
        [SerializeField] private LocationManager _locationManager;
        [SerializeField] private Location _location;
        private Button _button;

        private void Awake() {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(delegate { _locationManager.ChangeLocation(_location); });
            _locationManager.LocationChanged += ChangeMode;
        }

        private void ChangeMode(Location newLocation) {
            bool isOurLocation = newLocation == _location;
            _button.interactable = !isOurLocation;
        }
    }
}
