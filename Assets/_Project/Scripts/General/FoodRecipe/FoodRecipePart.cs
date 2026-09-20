using TMPro;
using UnityEngine;

namespace MadnessCooking.General {
    public class FoodRecipePart : HoverItemNameActivator {
        [SerializeField] protected TextMeshProUGUI _countText;
        protected GrayscaleImage _grayscaleIcon;
        protected bool _isAvailable;

        public bool IsAvailable => _isAvailable || !gameObject.activeSelf;

        protected void CheckGrayscaleIcon() {
            if (_grayscaleIcon != null) return;
            _grayscaleIcon = _icon as GrayscaleImage;
        }

        public virtual void Setup(IngredientCount count, bool isAvailable) {
            CheckGrayscaleIcon();
            gameObject.SetActive(true);
            _isAvailable = isAvailable;

            _grayscaleIcon.Setup(count.Ingredient.Icon, !isAvailable);
            _countText.text = count.Count.ToString() + "x";
            _showingItem = count.Ingredient;
        }

        public virtual void Setup(Technic technic, bool isAvailable) {
            CheckGrayscaleIcon();
            gameObject.SetActive(true);
            _grayscaleIcon.Setup(technic.Icon, !isAvailable);
            _countText.text = "1x";
        }

        public virtual void Disable() {
            gameObject.SetActive(false);
        }
    }
}
