using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Pest : MonoBehaviour {
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private bool _isSpriteChanging;
    [SerializeField] private bool _isRotatable;
    [SerializeField] private bool _isMovable;
    private SpriteRenderer _renderer;

    public PestData Data { get; private set; }

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeState(bool value) {
        gameObject.SetActive(value);
    }

    public void Randomize(RangeVector position, int prefabIndex) {
        int spriteIndex = -1;
        if (_isSpriteChanging) {
            spriteIndex = Random.Range(0, _sprites.Length);
            _renderer.sprite = _sprites[spriteIndex];
        }

        if (_isMovable)
            transform.position = position.RandomValue;

        if (_isRotatable)
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));

        Vector2 normalizedPosition = position.InverseLerp(transform.position);
        Data = new(prefabIndex, spriteIndex, new(transform.rotation),
            new(transform.position), new(normalizedPosition));
    }

    public void Bind(PestData pestData) {
        Data = pestData;
        transform.SetPositionAndRotation(Data.Position.GetVector(),
            Data.RotationDegree.GetQuaternion());

        if (_isSpriteChanging)
            _renderer.sprite = _sprites[Data.SpriteIndex];
    }

    public Sprite GetSprite() => _renderer.sprite;
}
