using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    [RequireComponent(typeof(Pest))]
    public class PestAudio : MonoBehaviour {
        [SerializeField] private AudioSource _audio;
        private Pest _pest;

        private void Awake() {
            _pest = GetComponent<Pest>();
            _pest.PestRemoved += _audio.Play;
        }
    }
}
