using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LocationNotificatoinAudio : MonoBehaviour {
    [SerializeField] private LocationNotificationManager _manager;

    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _manager.NotificationCreated += delegate { _audioSource.Play(); };
    }
}
