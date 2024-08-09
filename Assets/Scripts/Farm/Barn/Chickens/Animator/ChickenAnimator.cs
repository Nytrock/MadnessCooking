using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChickenAnimator : MonoBehaviour {
    [SerializeField] private RangeVector _targets;
    [SerializeField, Min(0)] private float _step;
    [SerializeField] private Direction _startDirection;

    private bool _isFeed;
    private float _speed;

    private Vector2 _target;
    private Direction _direction;
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void Start() {
        SetDirection(_startDirection);
        transform.position = _target;
    }

    private void Update() {
        if (!_isFeed)
            return;

        transform.position = Vector2.MoveTowards(transform.position, _target, _step * _speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, _target) < 0.01f)
            SetDirection(_direction.Reverse());
    }

    private void SetDirection(Direction direction) {
        _direction = direction;
        transform.localScale = new Vector2(direction.ToFloat(), 1);

        if (_direction == Direction.Left)
            _target = _targets.LeftDown.position;
        else
            _target = _targets.RightUp.position;
    }

    public void SetSpeed(float speed) {
        _animator.SetFloat("speed", speed);
        _speed = speed;
    }

    public void ChangeState(bool isFeed) {
        _animator.SetBool("isFeed", isFeed);
        _isFeed = isFeed;
    }
}
