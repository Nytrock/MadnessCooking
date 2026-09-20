using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    [RequireComponent(typeof(AudioSource))]
    public class KitchenCatAudio : MonoBehaviour {
        [SerializeField] private KitchenCat _cat;

        private void Awake() {
            AudioSource audioSource = GetComponent<AudioSource>();
            _cat.Petted += audioSource.Play;
        }
    }
}
