using UnityEngine;

public class TableFoodView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        ResetSprite();
    }

    public void SetSprite(Sprite newSprite)
    {
        _spriteRenderer.sprite = newSprite;
    }

    public void ResetSprite()
    {
        _spriteRenderer.sprite = null;
    }
}
