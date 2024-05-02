using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LocationButton : MonoBehaviour
{
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private Transform _location;
    [SerializeField] private float _fatigueCoef;

    public Vector2 Location => _location.position;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate { _locationManager.ChangeLocation(this); });
        _locationManager.LocationChanged += ChangeMode;
    }

    private void ChangeMode(Vector2 newPosition)
    {
        var isOurLocation = newPosition == (Vector2)_location.position;
        if (isOurLocation)
            FatigueManager.instance.ChangeFatigue(_fatigueCoef);
        _button.interactable = !isOurLocation;
    }
}
