using UnityEngine;

namespace MadnessCooking.General {
    public class RemoteParticleSystem : MonoBehaviour {
        [SerializeField] private ParticleSystem _particleSystem;

        public void Play() {
            _particleSystem.Play();
        }

        public void Stop() {
            _particleSystem.Stop();
        }
    }
}
