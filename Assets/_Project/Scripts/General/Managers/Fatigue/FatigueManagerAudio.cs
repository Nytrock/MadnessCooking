using UnityEngine;

namespace MadnessCooking.General {
    [RequireComponent(typeof(FatigueManager))]
    public class FatigueManagerAudio : MonoBehaviour {
        [SerializeField] private AudioSource _tiredAudio;
        private FatigueManager _manager;

        private void Awake() {
            _manager = GetComponent<FatigueManager>();
            _manager.TiredChanged += PlayTiredAudio;
        }

        private void PlayTiredAudio(bool isTired) {
            if (!isTired)
                return;

            _tiredAudio.Play();
        }
    }
}
