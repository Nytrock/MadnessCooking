using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LocationButton : MonoBehaviour
{
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private Transform _location;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate { _locationManager.ChangeLocation(_location); });

        _locationManager.LocationChanged += ChangeMode;
    }

    private void ChangeMode(Transform newPosition)
    {
        _button.interactable = (Vector2)newPosition.position != (Vector2)_location.position;
    }
}
