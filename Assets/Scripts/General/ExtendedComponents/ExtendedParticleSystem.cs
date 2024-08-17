using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ExtendedParticleSystem : MonoBehaviour {
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

    public void SetColor(Color color) {
        ParticleSystem.MainModule main = _particleSystem.main;
        main.startColor = color;
    }
}
