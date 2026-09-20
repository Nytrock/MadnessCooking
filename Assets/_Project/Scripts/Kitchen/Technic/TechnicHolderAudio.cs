using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    public class TechnicHolderAudio : MonoBehaviour {
        [SerializeField] private TechnicHolder _technicHolder;
        [SerializeField] private AudioSource _cookAudio;

        private void Awake() {
            _technicHolder.CookChanged += UpdateCookAudio;
        }

        private void UpdateCookAudio() {
            _cookAudio.ForceChangeState(_technicHolder.Data.IsCooking);
        }
    }
}
