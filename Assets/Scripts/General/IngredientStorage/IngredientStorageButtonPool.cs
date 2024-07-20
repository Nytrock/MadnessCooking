using UnityEngine;

public class IngredientStorageButtonPool : Pool<IngredientStorageButton> {
    [SerializeField] private IngredientStorageButton _prefab;
    [SerializeField] private HoverText _hoverText;

    private void Awake() {
        _prefab.gameObject.SetActive(false);
    }

    public IngredientStorageButton GetObject(BuyableItemCount<Ingredient> count) {
        IngredientStorageButton button = GetObject();
        button.SetVisual(count);
        button.SetHoverText(_hoverText);
        button.gameObject.SetActive(true);
        return button;
    }

    public override IngredientStorageButton GetObject() {
        if (_pool.Count == 0)
            return Instantiate(_prefab, _container);
        return _pool.Dequeue();
    }

    public override void PutObject(IngredientStorageButton button) {
        _pool.Enqueue(button);
        button.gameObject.SetActive(false);
    }
}
