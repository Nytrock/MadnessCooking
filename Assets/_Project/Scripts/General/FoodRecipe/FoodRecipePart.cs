using TMPro;
using UnityEngine;

public class FoodRecipePart : HoverItemNameActivator {
    [SerializeField] protected TextMeshProUGUI _countText;
    protected GrayscaleImage _grayscaleIcon;
    protected bool _isAvailable;

    public bool IsAvailable => _isAvailable || !gameObject.activeSelf;

    private void GetGrayscaleIcon() {
        _grayscaleIcon = _icon as GrayscaleImage;
    }

    public virtual void Setup(BuyableItemCount<Ingredient> count, bool isAvailable) {
        if (_grayscaleIcon == null)
            GetGrayscaleIcon();

        gameObject.SetActive(true);
        _isAvailable = isAvailable;

        _grayscaleIcon.Setup(count.Item.Icon, !isAvailable);
        _countText.text = count.Count.ToString() + "x";
        _showingItem = count.Item;
    }

    public virtual void Setup(Technic technic, bool isAvailable) {
        if (_grayscaleIcon == null)
            GetGrayscaleIcon();

        gameObject.SetActive(true);
        _grayscaleIcon.Setup(technic.Icon, !isAvailable);
        _countText.text = "1x";
    }

    public virtual void Disable() {
        gameObject.SetActive(false);
    }
}
