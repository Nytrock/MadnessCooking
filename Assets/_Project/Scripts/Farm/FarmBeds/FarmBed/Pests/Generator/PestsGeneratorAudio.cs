using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class PestsGeneratorAudio : MonoBehaviour {
        [SerializeField] private PestsGenerator _generator;
        [SerializeField] private AudioSource _cleanAudio;

        private void Awake() {
            _generator.PestsCleaned += Play;
        }

        private void Play() {
            if (!gameObject.activeInHierarchy)
                return;

            _cleanAudio.Play();
        }
    }
}
