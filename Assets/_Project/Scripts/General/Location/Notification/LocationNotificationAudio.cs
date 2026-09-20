using UnityEngine;

namespace MadnessCooking.General {
    [RequireComponent(typeof(AudioSource))]
    public class LocationNotificationAudio : MonoBehaviour {
        [SerializeField] private LocationNotificationManager _manager;

        private AudioSource _audioSource;

        private void Awake() {
            _audioSource = GetComponent<AudioSource>();
            _manager.NotificationCreated += delegate { _audioSource.Play(); };
        }
    }
}
