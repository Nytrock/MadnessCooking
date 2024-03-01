using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Pest : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private bool _isRotatable;
    [SerializeField] private bool _isMovable;
    private SpriteRenderer _renderer;

    [HideInInspector] public Transform LeftDown;
    [HideInInspector] public Transform RightUp;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeState(bool value)
    {
        gameObject.SetActive(value);
    }

    public void SetupBorders(Transform leftDown, Transform rightUp)
    {
        LeftDown = leftDown;
        RightUp = rightUp;
    }

    public void Randomize()
    {
        _renderer.sprite = _sprites[Random.Range(0, _sprites.Length)];

        if (_isMovable)
            transform.position = new Vector2(Random.Range(LeftDown.position.x, RightUp.position.x),
                Random.Range(LeftDown.position.y, RightUp.position.y));
        
        if (_isRotatable)
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
    }

    public Sprite GetSprite()
    {
        return _renderer.sprite;
    }
}
