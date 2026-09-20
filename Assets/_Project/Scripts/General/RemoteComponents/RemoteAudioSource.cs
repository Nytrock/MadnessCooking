using UnityEngine;

namespace MadnessCooking.General {
    public class RemoteAudioSource : MonoBehaviour {
        [SerializeField] private AudioSource _audioSource;

        public void Play() {
            _audioSource.Play();
        }

        public void Stop() {
            _audioSource.Stop();
        }
    }
}
