using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class Pest : MonoBehaviour {
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private bool _isSpriteChanging;
    [SerializeField] private bool _isRotatable;
    [SerializeField] private bool _isMovable;
    private SpriteRenderer _renderer;

    [field: SerializeField] public PestData Data { get; private set; }

    public event Action PestRemoved;

    public void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeState(bool value) {
        if (value)
            gameObject.SetActive(value);
        else
            _renderer.sprite = null;
    }

    public void Remove() {
        PestRemoved?.Invoke();
    }

    public void Randomize(RangeVector localPosition, RangeVector globalPosition, int prefabIndex) {
        int spriteIndex = -1;
        if (_isSpriteChanging) {
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
        Data = new(prefabIndex, spriteIndex, new(transform.rotation),
            new(transform.position), new(normalizedPosition));
    }

    public void Bind(PestData pestData) {
        Data = pestData;
        transform.SetPositionAndRotation(Data.Position,
            Data.RotationDegree);

        if (_isSpriteChanging)
            _renderer.sprite = _sprites[Data.SpriteIndex];
    }

    public Sprite GetSprite() => _renderer.sprite;
}
