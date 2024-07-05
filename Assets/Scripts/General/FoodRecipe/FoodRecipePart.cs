using TMPro;
using UnityEngine;

public class FoodRecipePart : MonoBehaviour {
    [SerializeField] protected GrayscaleImageRenderer _icon;
    [SerializeField] protected TextMeshProUGUI _countText;

    public virtual void Setup(BuyableItemCount<Ingredient> count, bool isAvailable) {
        gameObject.SetActive(true);
        _icon.Setup(count.Item.Icon, !isAvailable);
        _countText.text = count.Count.ToString() + "x";
    }

    public virtual void Setup(Technic technic, bool isAvailable) {
        gameObject.SetActive(true);
        _icon.Setup(technic.Icon, !isAvailable);
        _countText.text = "1x";
    }
}
