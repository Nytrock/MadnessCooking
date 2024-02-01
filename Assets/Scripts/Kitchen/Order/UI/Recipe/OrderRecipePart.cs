using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderRecipePart : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _count;

    public void Setup(IngredientCount count, bool isHave)
    {
        gameObject.SetActive(true);
        _icon.sprite = count.Ingredient.IngredientSprite;
        _count.text = count.Count.ToString() + "x";

        if (isHave)
            _count.color = Color.white;
        else 
            _count.color = Color.red;
    }

    public void Setup(Technic technic, bool isFree)
    {
        gameObject.SetActive(true);
        _icon.sprite = technic.MiniSprite;
        _count.text = "1x";

        if (isFree)
            _count.color = Color.white;
        else
            _count.color = Color.red;
    }
}
