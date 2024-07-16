using UnityEngine;

public class OrderRenderer : MonoBehaviour {
    [SerializeField] private Transform _sprite;
    [SerializeField] private Transform _leftDownCorner;
    [SerializeField] private Transform _rightUpCorner;
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;
    private Order _order;

    public Order Order => _order;

    private void Awake() {
        if (_minAngle > _maxAngle)
            (_minAngle, _maxAngle) = (_maxAngle, _minAngle);
        Disable();
    }

    public void Enable(Order order) {
        _order = order;
        RandomizePosition();
        _sprite.gameObject.SetActive(true);
    }

    private void RandomizePosition() {
        float x = Random.Range(_leftDownCorner.position.x, _rightUpCorner.position.x);
        float y = Random.Range(_leftDownCorner.position.y, _rightUpCorner.position.y);
        _sprite.position = new Vector2(x, y);
        _sprite.rotation = Quaternion.Euler(0, 0, Random.Range(_minAngle, _maxAngle));
    }

    public void Disable() {
        _order = null;
        _sprite.gameObject.SetActive(false);
    }
}
