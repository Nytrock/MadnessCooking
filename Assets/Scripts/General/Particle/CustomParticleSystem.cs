using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CustomParticleSystem : MonoBehaviour {
    private ParticleSystem _particleSystem;

    private void Awake() {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void ChangeState(bool newValue) {
        if (newValue)
            _particleSystem.Play();
        else
            _particleSystem.Stop();
    }
}
