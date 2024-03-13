using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodRecipePart : MonoBehaviour
{
    [SerializeField] protected Image _icon;
    [SerializeField] protected TextMeshProUGUI _count;

    public virtual void Setup(IngredientCount count, bool isHave)
    {
        gameObject.SetActive(true);
        _icon.sprite = count.Ingredient.Icon;
        _count.text = count.Count.ToString() + "x";
    }

    public virtual void Setup(Technic technic, bool isFree)
    {
        gameObject.SetActive(true);
        _icon.sprite = technic.Icon;
        _count.text = "1x";
    }
}
