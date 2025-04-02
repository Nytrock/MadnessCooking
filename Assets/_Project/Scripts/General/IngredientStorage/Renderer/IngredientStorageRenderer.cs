using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class IngredientStorageRenderer : MonoBehaviour {
    [SerializeField, Min(1)] private int _needCount;
    [SerializeField] private IngredientStorage _ingredientStorage;
    [SerializeField] protected IngredientRenderer[] _ingredientsRenderers;

    protected List<IngredientRenderer> _availableIngredientRenderers;
    private readonly IngredientCountList _ingredientsRenderersCount = new();

    private void Awake() {
        _ingredientStorage.IngredientCountAdded += CheckAddedIngredient;
        _ingredientStorage.IngredientCountRemoved += CheckRemovedIngredient;
        _ingredientStorage.LoadingDataEnded += SetupNeedCountUpdating;

        _availableIngredientRenderers = _ingredientsRenderers.ToList();
        foreach (var renderer in _availableIngredientRenderers)
            renderer.Setup();
    }

    private void SetupNeedCountUpdating() {
        if (_ingredientStorage.Data.MaxSpace == -1)
            return;

        _ingredientStorage.Data.SpaceChanged += UpdateMaxSpace;
        UpdateMaxSpace();
    }

    private void UpdateMaxSpace() {
        if (_ingredientStorage.Data.MaxSpace == -1)
            return;

        int newNeedCount = _ingredientStorage.Data.MaxSpace / _ingredientsRenderers.Length;
        if (newNeedCount == _needCount)
            return;

        _needCount = newNeedCount;
        _availableIngredientRenderers.Clear();

        foreach (var renderer in _ingredientsRenderers) {
            renderer.Disable();
            _availableIngredientRenderers.Add(renderer);
        }
        _ingredientsRenderersCount.Clear();

        foreach (var ingredientCount in _ingredientStorage.Data.Ingredients)
            CheckAddedIngredient(ingredientCount);
    }

    private void CheckRemovedIngredient(IngredientCount removedCount) {
        int ingredientCount = _ingredientStorage.Data.GetIngredientCount(removedCount.Ingredient);
        int nowRenderersCount = _ingredientsRenderersCount.GetIngredientCount(removedCount.Ingredient);
        int newRenderersCount = ingredientCount / _needCount;

        int removedRenderersCount = nowRenderersCount - newRenderersCount;
        if (removedRenderersCount == 0)
            return;

        _ingredientsRenderersCount.Remove(removedCount.Ingredient, removedRenderersCount);
        for (int i = 0; i < _ingredientsRenderers.Length; i++) {
            if (removedRenderersCount == 0)
                break;

            while (_ingredientsRenderers[i].Ingredient == removedCount.Ingredient) {
                if (removedRenderersCount == 0)
                    break;

                DisableIngredientRenderer(i);
                removedRenderersCount--;
            }
        }
    }

    private void CheckAddedIngredient(IngredientCount addedCount) {
        int ingredientCount = _ingredientStorage.Data.GetIngredientCount(addedCount.Ingredient);
        int nowRenderersCount = _ingredientsRenderersCount.GetIngredientCount(addedCount.Ingredient);
        int newRenderersCount = ingredientCount / _needCount;

        int addedRenderersCount = newRenderersCount - nowRenderersCount;
        if (addedRenderersCount == 0)
            return;

        _ingredientsRenderersCount.Add(addedCount.Ingredient, addedRenderersCount);
        for (int i = 0; i < addedRenderersCount; i++)
            EnableIngredientRenderer(addedCount.Ingredient);
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
