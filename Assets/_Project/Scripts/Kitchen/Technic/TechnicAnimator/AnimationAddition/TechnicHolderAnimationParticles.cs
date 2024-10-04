using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class TechnicHolderAnimationParticles : TechnicHolderAnimationAddition {
    private ParticleSystem _particleSystem;

    private void Awake() {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public override void UpdateAnimation(TechnicHolderData data, bool isTest = false) {
        if (isTest) {
            _particleSystem.ChangeState(!_particleSystem.isPlaying);
            return;
        }

        if (data.IsCooking)
            _particleSystem.SetColor(data.NowOrder.Food.Color);
        _particleSystem.ChangeState(data.IsCooking);
    }
}
