using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _count;

    public void SetVisual(IngredientCount count)
    {
        _icon.sprite = count.Ingredient.IngredientSprite;
        _count.text = count.Count.ToString();
        gameObject.SetActive(true);
    }

    public void SetCount(int count)
    {
        _count.text = count.ToString();
    }
}
