using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class IngredientStorageRenderer : MonoBehaviour {
    [SerializeField, Min(1)] private int _needCount;
    [SerializeField] private IngredientStorage _ingredientStorage;
    [SerializeField] protected IngredientRenderer[] _ingredientsRenderers;

    protected List<IngredientRenderer> _availableIngredientRenderers;
    private readonly BuyableItemCountList<Ingredient> _ingredients = new();

    private void Awake() {
        _ingredientStorage.IngredientCountAdded += CheckAddedIngredient;
        _ingredientStorage.IngredientCountRemoved += CheckRemovedIngredient;

        _availableIngredientRenderers = _ingredientsRenderers.ToList();
        foreach (var renderer in _availableIngredientRenderers)
            renderer.Setup();
    }

    private void CheckRemovedIngredient(BuyableItemCount<Ingredient> removedCount) {
        _ingredients.Remove(removedCount);
        int removedRenderersCount = removedCount.Count / _needCount;

        if (removedRenderersCount == 0)
            return;

        for (int i = 0; i < _ingredientsRenderers.Length; i++) {
            if (removedRenderersCount == 0)
                break;

            if (_ingredientsRenderers[i].Ingredient == removedCount.Item) {
                DisableIngredientRenderer(i);
                removedRenderersCount--;
            }
        }
    }

    private void CheckAddedIngredient(BuyableItemCount<Ingredient> addedCount) {
        _ingredients.Add(new(addedCount));
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

    protected virtual void DisableIngredientRenderer(int index) {
        IngredientRenderer renderer = _ingredientsRenderers[index];
        renderer.Disable();
        _availableIngredientRenderers.Add(renderer);
    }

    protected abstract int GetIngredientIndex();
}
