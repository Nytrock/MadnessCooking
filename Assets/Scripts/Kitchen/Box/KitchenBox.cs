using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class KitchenBox : MonoBehaviour {
    [SerializeField] private Sprite _closedBox;
    [SerializeField] private Sprite _openedBox;
    private SpriteRenderer _renderer;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = _closedBox;
    }

    public void OpenBox() {
        _renderer.sprite = _openedBox;
    }
}
