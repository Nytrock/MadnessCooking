using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientStorageButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _count;

    public void SetVisual(IngredientCount count)
    {
        _icon.sprite = count.Ingredient.Icon;
        _count.text = count.Count.ToString();
        gameObject.SetActive(true);
    }

    public void SetCount(int count)
    {
        _count.text = count.ToString();
    }
}
