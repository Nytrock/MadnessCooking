using UnityEngine;

public class LocationAdditionUI : MonoBehaviour
{
    [SerializeField] private Transform _point;
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private GameObject _UI;

    private void Start()
    {
        _locationManager.LocationChanged += UpdateUI;
        UpdateUI(_locationManager.MainCamera);
    }

    private void UpdateUI(Transform newPosition)
    {
        _UI.SetActive(_point.position.x == newPosition.position.x);
    }

    public void ChangeUIState(bool newValue)
    {
        _UI.SetActive(newValue);
    }
}
