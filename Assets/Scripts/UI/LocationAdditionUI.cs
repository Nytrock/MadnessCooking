using UnityEngine;

public class LocationAdditionUI : MonoBehaviour
{
    [SerializeField] private CameraManager manager;
    [SerializeField] private LocationManager locationManager;

    private void Start()
    {
        locationManager.LocationChanged += UpdateUI;
    }

    private void UpdateUI(Transform newPosition)
    {
        gameObject.SetActive(manager.transform.position.x == newPosition.position.x);
    }
}
