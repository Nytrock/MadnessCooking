using MadnessCooking.General;
using UnityEngine;

namespace MadnessCooking.Farm {
    public class Cow : NeedHoldAdd {
        [SerializeField] private IngredientManager _ingredientsManager;
        [SerializeField] private Puncher _puncher;
        [SerializeField, Min(0)] private float _wasteActiveAmount;

        protected override void AddReady() {
            _puncher.AddWaste(_wasteActiveAmount);
            base.AddReady();
        }

        public override void LoadSave(GameData data) {
            data.Farm.Cow ??= new(_readyDefaultCount, _materialDefaultCount);
            Data = data.Farm.Cow;
        }

        protected override void UpdateUpgrades() {
            base.UpdateUpgrades();
            if (Data.IsUnlocked)
                _ingredientsManager.AddItem(ConstIngredients.Instance.Milk);
        }
    }
}
