using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class IngredientStorageRenderer : MonoBehaviour {
    [SerializeField, Min(1)] private int _needCount;
    [SerializeField] private IngredientStorage _ingredientStorage;
    [SerializeField] private IngredientRenderer[] _ingredientsRenderers;

    protected List<IngredientRenderer> _availableIngredientRenderers;
    private BuyableItemCountList<Ingredient> _ingredients;

    private void Awake() {
        _ingredientStorage.IngredientCountAdded += CheckAddedIngredient;
        _ingredientStorage.IngredientCountAdded += CheckRemovedIngredient;
        _availableIngredientRenderers = _ingredientsRenderers.ToList();
    }

    private void CheckRemovedIngredient(BuyableItemCount<Ingredient> removedCount) {
        _ingredients.Remove(removedCount);
        int removedRenderersCount = removedCount.Count / _needCount;

        if (removedRenderersCount == 0)
            return;

        foreach (var ingredientRenderer in _ingredientsRenderers) {
            if (removedRenderersCount == 0)
                break;

            if (ingredientRenderer.Ingredient == removedCount.Item) {
                DisableIngredientRenderer(ingredientRenderer);
                removedRenderersCount--;
            }
        }
    }

    private void CheckAddedIngredient(BuyableItemCount<Ingredient> addedCount) {
        _ingredients.Add(addedCount);
        int nowCount = _ingredients.GetItemCount(addedCount.Item);
        int addedRenderersCount = nowCount / _needCount;

        if (addedRenderersCount == 0)
            return;

        _ingredients.Remove(addedCount.Item, _needCount * addedRenderersCount);
        for (int i = 0; i < addedRenderersCount; i++)
            EnableIngredientRenderer(addedCount.Item);
    }

    private void EnableIngredientRenderer(Ingredient ingredient) {
        if (_availableIngredientRenderers.Count == 0)
            return;

        int index = GetIngredientIndex();
        IngredientRenderer renderer = _availableIngredientRenderers[index];
        _availableIngredientRenderers.RemoveAt(index);
        renderer.SetSprite(ingredient);
    }

    private void DisableIngredientRenderer(IngredientRenderer ingredientRenderer) {
        ingredientRenderer.Disable();
        _availableIngredientRenderers.Add(ingredientRenderer);
    }

    protected abstract int GetIngredientIndex();
}
