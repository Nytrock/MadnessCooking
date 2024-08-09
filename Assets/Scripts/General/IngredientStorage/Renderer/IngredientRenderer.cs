using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IngredientRenderer : MonoBehaviour {
    protected SpriteRenderer _renderer;
    private Ingredient _ingredient;

    public Ingredient Ingredient => _ingredient;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
        Disable();
    }

    public void Disable() {
        _renderer.sprite = null;
        _ingredient = null;
    }

    public virtual void SetSprite(Ingredient ingredient) {
        _renderer.sprite = ingredient.MiniSprite;
        _ingredient = ingredient;
    }
}
