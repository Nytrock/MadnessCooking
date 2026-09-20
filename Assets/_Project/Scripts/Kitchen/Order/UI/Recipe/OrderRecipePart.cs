using UnityEngine;
using MadnessCooking.Farm;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    public class OrderRecipePart : FoodRecipePart {
        [SerializeField] private TextAvailableRenderer _countTextRenderer;
        [SerializeField] private Sprite _moneySprite;
        private IngredientCount _ingredientCount;

        public IngredientCount IngredientCount => _ingredientCount;

        public override void Setup(IngredientCount count, bool isAvailable) {
            base.Setup(count, isAvailable);
            _countTextRenderer.UpdateAvailable(isAvailable);
            _ingredientCount = count;
        }

        public void SetupAutoSpice(IngredientCount ingredientCount) {
            gameObject.SetActive(true);
            _ingredientCount = ingredientCount;
            _showingItem = ConstIngredients.Instance.Money;
            UpdateAutoSpiceStatus(MoneyManager.Instance.MoneyCount);
        }

        public void UpdateAutoSpiceStatus(int moneyCount) {
            int price = _ingredientCount.Count * _ingredientCount.Ingredient.Price;
            bool isMoneyEnough = moneyCount >= price;
            _isAvailable = isMoneyEnough;

            CheckGrayscaleIcon();
            _grayscaleIcon.Setup(_moneySprite, !isMoneyEnough);
            _countText.text = price.ToString() + "x";
            _countTextRenderer.UpdateAvailable(isMoneyEnough);
        }

        public override void Setup(Technic technic, bool isAvailable) {
            base.Setup(technic, isAvailable);
            _countTextRenderer.UpdateAvailable(isAvailable);
        }

        public void UpdateAvailable(bool isAvailable) {
            _isAvailable = isAvailable;
            _countTextRenderer.UpdateAvailable(isAvailable);
            _grayscaleIcon.SetGrayscaleVisibility(!isAvailable);
        }
    }
}
