using UnityEngine;

public class OrderRenderer : MonoBehaviour {
    [SerializeField] private Transform _sprite;
    [SerializeField] private RangeVector _position;
    [SerializeField] private RangeFloat _angle;
    private Order _order;

    public Order Order => _order;

    private void Awake() {
        Disable();
    }

    public void Enable(Order order) {
        _order = order;
        RandomizePosition();
        _sprite.gameObject.SetActive(true);
    }

    [ContextMenu("RandomizePosition")]
    private void RandomizePosition() {
        _sprite.SetPositionAndRotation(_position.RandomValue, Quaternion.Euler(0, 0, _angle.RandomValue));
    }

    public void Disable() {
        _order = null;
        _sprite.gameObject.SetActive(false);
    }
}
