using UnityEngine;

[RequireComponent(typeof(MoneyManager))]
public class MoneyManagerAudio : MonoBehaviour {
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private LocationAndOutputAudioInfo[] _locationSounds;
    [SerializeField] private AudioSource _audioSource;

    private MoneyManager _manager;

    private void Awake() {
        _manager = GetComponent<MoneyManager>();
        _manager.MoneyChanged += PlaySound;
        _locationManager.LocationChanged += ChangeLocation;
    }

    private void ChangeLocation(Location location) {
        foreach (var locationAudio in _locationSounds) {
            if (locationAudio.ContainsLocation(location)) {
                SetAudio(locationAudio);
                return;
            }
        }

        SetAudio(null);
    }

    private void SetAudio(LocationAudioInfo locationAudio) {
        _audioSource.SetAudioInfo(locationAudio);
    }

    private void PlaySound(int count) {
        _audioSource.Play();
    }
}
