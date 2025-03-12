using UnityEngine;

public class IngredientCountButtonPool : Pool<IngredientCountButton> {
    [SerializeField] private IngredientCountButton _prefab;
    [SerializeField] private HoverTextPanel _hoverText;

    public IngredientCountButton GetObject(IngredientCount count) {
        IngredientCountButton button = GetObject();
        button.SetVisual(count);
        button.gameObject.SetActive(true);
        return button;
    }

    protected override IngredientCountButton CreateObject() {
        IngredientCountButton button = Instantiate(_prefab, _container);
        button.SetHoverPanel(_hoverText);
        return button;
    }

    public override void PutObject(IngredientCountButton button) {
        base.PutObject(button);
        button.ResetVisual();
        button.gameObject.SetActive(false);
    }
}
