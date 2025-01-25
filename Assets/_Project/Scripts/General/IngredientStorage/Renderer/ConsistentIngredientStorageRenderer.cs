public class ConsistentIngredientStorageRenderer : IngredientStorageRenderer {
    protected override void DisableIngredientRenderer(int index) {
        int i;
        for (i = index; i < _ingredientsRenderers.Length - 1; i++) {
            if (_ingredientsRenderers[i + 1].Ingredient == null)
                break;
            _ingredientsRenderers[i].SetSprite(_ingredientsRenderers[i + 1].Ingredient);
        }

        _ingredientsRenderers[i].Disable();
        _availableIngredientRenderers.Add(_ingredientsRenderers[i]);
    }

    protected override int GetIngredientIndex() {
        return 0;
    }
}
