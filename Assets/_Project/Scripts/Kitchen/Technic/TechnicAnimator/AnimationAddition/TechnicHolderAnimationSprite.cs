using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    [RequireComponent(typeof(SpriteRenderer))]
    public class TechnicHolderAnimationSprite : TechnicHolderAnimationAddition {
        private SpriteRenderer _renderer;

        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public override void UpdateAnimation(TechnicHolderData data, bool isTest = false) {
            if (isTest)
                return;

            if (data.IsCooking)
                _renderer.sprite = data.NowOrder.Food.CookingSprite;
            else
                _renderer.sprite = null;
        }
    }
}
