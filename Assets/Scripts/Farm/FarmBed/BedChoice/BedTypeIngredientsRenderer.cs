using UnityEngine;
using UnityEngine.UI;

public class BedTypeIngredientsRenderer : MonoBehaviour
{
    [SerializeField] private Image[] _ingredientImages;
    [SerializeField] private Material _grayscaleMaterial;
    [SerializeField] private IngredientsManager _ingredientsManager;

    public void ShowIngredients(BedType bedType)
    {
        int index = 0;
        foreach (var ingredient in _ingredientsManager.GetAllIngredientsOfBedType(bedType)) {
            _ingredientImages[index].sprite = ingredient.Icon;
            if (_ingredientsManager.HaveIngredient(ingredient))
                _ingredientImages[index].material = null;
            else
                _ingredientImages[index].material = _grayscaleMaterial;
            _ingredientImages[index].gameObject.SetActive(true);
            index++;
        }

        for (int i = 0; i < _ingredientImages.Length; i++)
            _ingredientImages[i].gameObject.SetActive(false);
    }

    public bool HaveIngredients(BedType bedType)
    {
        return _ingredientsManager.HaveIngredientsOfBedType(bedType);
    }
}
