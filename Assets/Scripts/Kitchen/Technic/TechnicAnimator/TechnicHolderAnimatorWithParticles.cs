using UnityEngine;

public class TechnicHolderAnimatorWithParticles : TechnicHolderAnimator {
    [SerializeField] private ExtendedParticleSystem _particles;

    public override void UpdateAnimation() {
        base.UpdateAnimation();
        if (_data.IsCooking)
            _particles.SetColor(_data.NowOrder.Food.Color);
        _particles.ChangeState(_data.IsCooking);
    }
}
