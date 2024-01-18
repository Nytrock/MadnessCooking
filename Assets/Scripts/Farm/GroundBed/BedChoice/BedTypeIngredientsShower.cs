using UnityEngine;
using UnityEngine.UI;

public class BedTypeIngredientsShower : MonoBehaviour
{
    [SerializeField] private Image[] _ingredientImages;
    [SerializeField] private Material _grayscaleMaterial;
    [SerializeField] private IngredientsManager _ingredientsManager;

    public void ShowIngredients(BedType bedType)
    {
        var ingredients = SaveManager.instance.GetIngredientsOfOneBedType(bedType);
        for (int i = 0; i < _ingredientImages.Length; i++) {
            if (i < ingredients.Count) {
                _ingredientImages[i].sprite = ingredients[i].IngredientSprite;
                if (_ingredientsManager.HaveIngredient(ingredients[i]))
                    _ingredientImages[i].material = null;
                else
                    _ingredientImages[i].material = _grayscaleMaterial;
            }
            _ingredientImages[i].gameObject.SetActive(i < ingredients.Count);
        }
    }

    public bool HaveIngredients(BedType bedType)
    {
        return _ingredientsManager.GetIngredientsOfOneBedType(bedType).Count != 0;
    }
}
