using UnityEngine;

namespace MadnessCooking.General {
    [RequireComponent(typeof(AudioSource))]
    public class PausableAudio : MonoBehaviour {
        [SerializeField] private AudioSource _audioSource;

        private void Start() {
            PauseManager.Instance.PauseChanged += ChangeState;
        }

        private void ChangeState(bool isPause) {
            if (_audioSource == null)
                return;

            _audioSource.ChangeState(!isPause);
        }
    }
}
