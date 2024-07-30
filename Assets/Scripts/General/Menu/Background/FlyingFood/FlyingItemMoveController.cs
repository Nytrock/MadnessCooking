using UnityEngine;

public class FlyingItemMoveController : MonoBehaviour {
    [SerializeField] private Transform _leftEdge;
    [SerializeField] private Transform _rightEdge;
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _minForce;
    [SerializeField] private float _maxForce;

    public void SetRandomMove(FlyingItem flyingItem) {
        float randomPos = Random.Range(_leftEdge.position.x, _rightEdge.position.x);
        flyingItem.transform.position = new(randomPos, _leftEdge.position.y);

        float angle = Random.Range(_minAngle, _maxAngle) * Mathf.Deg2Rad;
        float force = Random.Range(_minForce, _maxForce);

        Vector2 direction = new(Mathf.Cos(angle) * force, Mathf.Sin(angle) * force);
        flyingItem.AddForce(direction);
    }
}
