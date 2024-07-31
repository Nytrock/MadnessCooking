using UnityEngine;

public class FlyingItemMoveController : MonoBehaviour {
    [SerializeField] private RangeVector _position;
    [SerializeField] private RangeFloat _angle;
    [SerializeField] private RangeFloat _force;

    public void SetRandomMove(FlyingItem flyingItem) {
        flyingItem.transform.position = _position.RandomValue;

        float angle = _angle.RandomValue * Mathf.Deg2Rad;
        float force = _force.RandomValue;

        Vector2 direction = new(Mathf.Cos(angle) * force, Mathf.Sin(angle) * force);
        flyingItem.AddForce(direction);
    }
}
