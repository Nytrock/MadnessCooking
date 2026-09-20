using System;
using UnityEngine;
using Random = UnityEngine.Random;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    [RequireComponent(typeof(SpriteRenderer))]
    public class Pest : MonoBehaviour {
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private bool _isSpriteChanging;
        [SerializeField] private bool _isRotatable;
        [SerializeField] private bool _isMovable;
        private SpriteRenderer _renderer;

        public PestData Data { get; private set; }

        public event Action PestRemoved;

        public void ChangeState(bool value) {
            CheckRenderer();
            _renderer.enabled = value;
        }

        private void CheckRenderer() {
            if (_renderer != null) return;

            _renderer = GetComponent<SpriteRenderer>();
        }

        public void Remove() {
            PestRemoved?.Invoke();
        }

        public void Randomize(RangeVector localPosition, RangeVector globalPosition, int prefabIndex) {
            int spriteIndex = -1;
            if (_isSpriteChanging) {
                CheckRenderer();
                spriteIndex = Random.Range(0, _sprites.Length);
                _renderer.sprite = _sprites[spriteIndex];
            }

            if (Data is not null)
                prefabIndex = Data.PrefabIndex;

            if (_isMovable)
                transform.position = localPosition.RandomValue;

            if (_isRotatable)
                transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));

            Vector2 normalizedPosition = globalPosition.InverseLerp(transform.position);
            Data = new(prefabIndex, spriteIndex, transform.rotation,
                transform.position, normalizedPosition);
        }

        public void Bind(PestData pestData) {
            CheckRenderer();
            Data = pestData;

            if (_isRotatable)
                transform.rotation = Data.RotationDegree;
            if (_isMovable)
                transform.position = Data.Position;
            if (_isSpriteChanging && Data.SpriteIndex != -1)
                _renderer.sprite = _sprites[Data.SpriteIndex];
        }

        public Sprite GetSprite() => _renderer.sprite;
    }
}
