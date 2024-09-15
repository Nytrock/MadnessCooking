using UnityEngine;

public class IngredientStorageButtonPool : Pool<IngredientStorageButton> {
    [SerializeField] private IngredientStorageButton _prefab;
    [SerializeField] private HoverItemName _hoverText;

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

    protected override IngredientStorageButton CreateObject() {
        return Instantiate(_prefab, _container);
    }

    public override void PutObject(IngredientStorageButton button) {
        base.PutObject(button);
        button.gameObject.SetActive(false);
    }
}
