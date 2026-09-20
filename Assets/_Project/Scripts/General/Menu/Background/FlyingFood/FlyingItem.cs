using System;
using UnityEngine;

namespace MadnessCooking.General {
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class FlyingItem : MonoBehaviour {
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        private float _bottomPosition;

        public event Action<FlyingItem> BottomReached;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            if (transform.position.y < _bottomPosition)
                BottomReached?.Invoke(this);
        }

        public void SetSprite(BuyableItem item) {
            _spriteRenderer.sprite = item.Icon;
        }

        public void SetBottomPosition(float bottomPosition) {
            _bottomPosition = bottomPosition;
        }

        public void ChangeState(bool newState) {
            gameObject.SetActive(newState);
        }

        public void AddForce(Vector2 force) {
            _rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
