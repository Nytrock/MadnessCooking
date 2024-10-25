using UnityEngine;

public class RemoteParticleSystem : MonoBehaviour {
    [SerializeField] private ParticleSystem _particleSystem;

    public void Play() {
        _particleSystem.Play();
    }

    public void Pause() {
        _particleSystem.Pause();
    }
}
