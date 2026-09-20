using System.Collections.Generic;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    [RequireComponent(typeof(BedTypeStyleUpdater))]
    public class IngredientChoiceUI : ChoiceUI<Ingredient, IngredientChoiceButton> {
        [SerializeField] private IngredientManager _ingredientsManager;
        [SerializeField] private TutorialManager _tutorialManager;

        private BedTypeStyleUpdater _renderer;
        private FarmBed _changingBed;

        protected override void Awake() {
            base.Awake();
            _renderer = GetComponent<BedTypeStyleUpdater>();
        }

        public void ActivateIngredientChoice(FarmBed farmBed) {
            if (_tutorialManager.IsWork)
                _tutorialManager.NextTutorialPart();

            _changingBed = farmBed;
            _renderer.UpdateStyle(_changingBed.Data.BedType);
            GenerateChoiceButtons();
            Activate();
        }

        protected override void GenerateChoiceButtons() {
            DestoyOldButtons();
            base.GenerateChoiceButtons();
        }

        protected override IngredientChoiceButton GenerateChoiceButton(Ingredient item) {
            IngredientChoiceButton button = base.GenerateChoiceButton(item);
            button.SetBedType(_changingBed.Data.BedType);
            return button;
        }

        protected override IEnumerable<Ingredient> GetItems() {
            return _ingredientsManager.GetAvailableIngredientsOfBedType(_changingBed.Data.BedType);
        }

        public override void SubmitChoice() {
            if (_tutorialManager.IsWork)
                _tutorialManager.NextTutorialPart();
            _changingBed.SetIngredient(_choosedButton.Item);
            Disable();
        }

        public override void SelectButton(IngredientChoiceButton button) {
            base.SelectButton(button);
            if (_tutorialManager.IsWork)
                _tutorialManager.NextTutorialPart();
        }

        public override void Disable() {
            base.Disable();
            _changingBed = null;
        }
    }
}
