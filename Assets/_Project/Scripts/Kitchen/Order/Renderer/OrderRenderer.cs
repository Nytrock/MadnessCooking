using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    public class OrderRenderer : MonoBehaviour {
        [SerializeField] private Transform _sprite;
        [SerializeField] private RangeVector _position;
        [SerializeField] private RangeFloat _angle;

        private void Awake() {
            ChangeState(false);
        }

        public void ChangeState(bool newState) {
            if (newState)
                RandomizePosition();
            _sprite.gameObject.SetActive(newState);
        }

        [ContextMenu(nameof(RandomizePosition))]
        private void RandomizePosition() {
            _sprite.SetPositionAndRotation(_position.RandomValue, Quaternion.Euler(0, 0, _angle.RandomValue));
        }
    }
}
