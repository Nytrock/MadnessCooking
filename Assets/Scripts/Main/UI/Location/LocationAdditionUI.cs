using UnityEngine;

public class LocationAdditionUI : MonoBehaviour
{
    [SerializeField] private Transform _point;
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private GameObject _UI;

    private void Awake()
    {
        _locationManager.LocationChanged += UpdateUI;
    }

    private void UpdateUI(Vector2 newPosition)
    {
        _UI.SetActive(_point.position.x == newPosition.x);
    }

    public void ChangeUIState(bool newValue)
    {
        _UI.SetActive(newValue);
    }
}
