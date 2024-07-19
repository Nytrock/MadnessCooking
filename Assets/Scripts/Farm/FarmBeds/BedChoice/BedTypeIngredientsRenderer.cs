using UnityEngine;

public class BedTypeIngredientsRenderer : MonoBehaviour {
    [SerializeField] private GrayscaleImageRenderer[] _ingredientImages;
    [SerializeField] private IngredientsManager _ingredientsManager;

    public void ShowIngredients(BedType bedType) {
        int index = 0;
        foreach (var ingredient in _ingredientsManager.GetAllIngredientsOfBedType(bedType)) {
            bool isAvailable = _ingredientsManager.HaveIngredient(ingredient);
            _ingredientImages[index].Setup(ingredient.Icon, !isAvailable);
            _ingredientImages[index].SetActive(true);
            index++;
        }

        for (int i = index; i < _ingredientImages.Length; i++)
            _ingredientImages[i].SetActive(false);
    }

    public bool HaveIngredients(BedType bedType) {
        return _ingredientsManager.HaveIngredientsOfBedType(bedType);
    }
}
