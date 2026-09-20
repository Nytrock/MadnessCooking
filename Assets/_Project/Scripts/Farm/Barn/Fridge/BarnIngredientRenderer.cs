using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class BarnIngredientRenderer : IngredientRenderer {
        [SerializeField] private Sprite _milkSprite;

        public override void SetSprite(Ingredient ingredient) {
            base.SetSprite(ingredient);
            _renderer.sprite = _milkSprite;
        }
    }
}
