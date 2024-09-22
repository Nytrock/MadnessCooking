using UnityEngine;

public class TechnicHolderAnimatorWithParticles : TechnicHolderAnimator {
    [SerializeField] private ParticleSystem _particles;

    public override void UpdateAnimation() {
        base.UpdateAnimation();
        if (_data.IsCooking) {
            _particles.SetColor(_data.NowOrder.Food.Color);
        }

        _particles.ChangeState(_data.IsCooking);
    }

    public override void TestAnimation() {
        base.TestAnimation();

        bool isCooking = _animator.GetBool("isCooking");
        _particles.ChangeState(isCooking);
    }
}
