using UnityEngine;

public class LocationAdditionUI : MonoBehaviour
{
    [SerializeField] private CameraManager manager;
    [SerializeField] private LocationManager locationManager;
    [SerializeField] private GameObject _UI;

    private void Start()
    {
        locationManager.LocationChanged += UpdateUI;
        UpdateUI(locationManager.MainCamera);
    }

    private void UpdateUI(Transform newPosition)
    {
        _UI.SetActive(manager.transform.position.x == newPosition.position.x);
    }
}
