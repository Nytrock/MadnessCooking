using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LocationButton : MonoBehaviour
{
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private Transform _location;
    [SerializeField] private float _fatigueCoef;

    public Transform Location => _location;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate { _locationManager.ChangeLocation(_location); });
        _locationManager.LocationChanged += ChangeMode;
    }

    private void ChangeMode(Transform newPosition)
    {
        var isOurLocation = (Vector2)newPosition.position == (Vector2)_location.position;
        if (isOurLocation)
            FatigueManager.instance.ChangeFatigue(_fatigueCoef);
        _button.interactable = !isOurLocation;
    }
}
