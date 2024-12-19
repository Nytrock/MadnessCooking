using TMPro;
using UnityEngine;

public class FoodRecipePart : HoverItemNameActivator {
    [SerializeField] protected GrayscaleImageRenderer _icon;
    [SerializeField] protected TextMeshProUGUI _countText;
    protected bool _isAvailable;

    public bool IsAvailable => _isAvailable || !gameObject.activeSelf;

    public virtual void Setup(BuyableItemCount<Ingredient> count, bool isAvailable) {
        gameObject.SetActive(true);
        _isAvailable = isAvailable;

        _icon.Setup(count.Item.Icon, !isAvailable);
        _countText.text = count.Count.ToString() + "x";
        _showingItem = count.Item;
    }

    public virtual void Setup(Technic technic, bool isAvailable) {
        gameObject.SetActive(true);
        _icon.Setup(technic.Icon, !isAvailable);
        _countText.text = "1x";
    }

    public virtual void Disable() {
        gameObject.SetActive(false);
    }
}
