using UnityEngine;

public class TechnicHolderAnimatorWithSprite : TechnicHolderAnimator {
    [SerializeField] private SpriteRenderer _renderer;

    public override void UpdateAnimation() {
        base.UpdateAnimation();
        if (_data.IsCooking)
            _renderer.sprite = _data.NowOrder.Food.CookingSprite;
        else
            _renderer.sprite = null;
    }
}
