using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodRecipePart : MonoBehaviour {
    [SerializeField] protected Image _icon;
    [SerializeField] protected TextMeshProUGUI _countText;

    public virtual void Setup(IngredientCount count, bool isHave) {
        gameObject.SetActive(true);
        _icon.sprite = count.Ingredient.Icon;
        _countText.text = count.Count.ToString() + "x";
    }

    public virtual void Setup(Technic technic, bool isFree) {
        gameObject.SetActive(true);
        _icon.sprite = technic.Icon;
        _countText.text = "1x";
    }
}
