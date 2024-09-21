using UnityEngine;

public class RandomIngredientStorageRenderer : IngredientStorageRenderer {
    protected override int GetIngredientIndex() {
        return Random.Range(0, _availableIngredientRenderers.Count);
    }
}
