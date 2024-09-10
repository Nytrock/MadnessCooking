using UnityEngine;

public class TechnicHolderAnimatorWithParticles : TechnicHolderAnimator {
    [SerializeField] private ParticleSystem _particles;
    [SerializeField, Min(0)] private float _playbackTime;

    public override void UpdateAnimation() {
        base.UpdateAnimation();
        if (_data.IsCooking) {
            _particles.SetColor(_data.NowOrder.Food.Color);
            _particles.SetPlaybackTime(_playbackTime);
        }

        _particles.ChangeState(_data.IsCooking);
    }

    public override void TestAnimation() {
        base.TestAnimation();

        bool isCooking = _animator.GetBool("isCooking");
        if (isCooking)
            _particles.SetPlaybackTime(_playbackTime);
        _particles.ChangeState(isCooking);
    }
}
