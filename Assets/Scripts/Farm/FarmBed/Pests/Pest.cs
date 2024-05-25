using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Pest : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private bool _isSpriteChanging;
    [SerializeField] private bool _isRotatable;
    [SerializeField] private bool _isMovable;
    private SpriteRenderer _renderer;

    public PestData PestData { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeState(bool value)
    {
        gameObject.SetActive(value);
    }

    public void Randomize(Vector2 leftDown, Vector2 rightUp, int prefabIndex)
    {
        PestData = new() {
            PrefabIndex = prefabIndex
        };

        if (_isSpriteChanging) {
            PestData.SpriteIndex = Random.Range(0, _sprites.Length);
            _renderer.sprite = _sprites[PestData.SpriteIndex];
        }

        if (_isMovable) {
            float x = Random.Range(leftDown.x, rightUp.x);
            float y = Random.Range(leftDown.y, rightUp.y);
            transform.position = new(x, y);
        }

        float xNormalized = Mathf.InverseLerp(leftDown.x, rightUp.x, transform.position.x);
        float yNormalized = Mathf.InverseLerp(leftDown.y, rightUp.y, transform.position.y);
        PestData.NormalizedPosition = new(xNormalized, yNormalized);
        PestData.Position = new(transform.position);

        if (_isRotatable)
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
        PestData.RotationDegree = new(transform.rotation);
    }

    public void Bind(PestData pestData)
    {
        PestData = pestData;
        transform.SetPositionAndRotation(PestData.Position.GetVector(), 
            PestData.RotationDegree.GetQuaternion());

        if (_isSpriteChanging)
            _renderer.sprite = _sprites[PestData.SpriteIndex];
    }

    public Sprite GetSprite() => _renderer.sprite;
}
