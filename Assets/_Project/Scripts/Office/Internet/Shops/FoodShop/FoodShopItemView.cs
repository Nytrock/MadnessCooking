using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Office {
    public class FoodShopItemView : BaseChooseShopItemView<Food> {
        [SerializeField] private FoodShopRecipe _recipeRenderer;
        [SerializeField] private HoverTextPanel _hoverText;

        protected override void Start() {
            base.Start();
            _recipeRenderer.SetHoverText(_hoverText);
        }

        protected override void SetInfo() {
            base.SetInfo();
            _recipeRenderer.SetupRecipe(_selectedItem);
        }

        public override void ResetInfo() {
            base.ResetInfo();
            _recipeRenderer.DisableParts();
        }
    }
}
