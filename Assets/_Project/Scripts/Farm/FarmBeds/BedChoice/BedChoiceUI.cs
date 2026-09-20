using System.Collections.Generic;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class BedChoiceUI : ChoiceBuyUI<BedType, BedChoiceButton> {
        [SerializeField] private BedTypeManager _bedTypesManager;
        [SerializeField] private BedTypeIngredientsRenderer _ingredientsRenderer;
        [SerializeField] private TutorialManager _tutorialManager;
        private BedChoice _changingBed;

        protected override void Awake() {
            base.Awake();
            _bedTypesManager.ItemAdded += UnblockBedType;
        }

        public void ActivateBedChoice(BedChoice newBed) {
            if (_tutorialManager.IsWork)
                _tutorialManager.NextTutorialPart();

            _changingBed = newBed;
            if (_choiceButtons.Count == 0)
                GenerateChoiceButtons();
            Activate();
        }

        protected override BedChoiceButton GenerateChoiceButton(BedType item) {
            BedChoiceButton button = base.GenerateChoiceButton(item);
            button.SetBlockedState(!_bedTypesManager.IsItemAvailable(item));
            return button;
        }

        protected override IEnumerable<BedType> GetItems() {
            return _bedTypesManager.GetAllItems();
        }

        private void UnblockBedType(BedType newType) {
            foreach (var button in _choiceButtons) {
                if (button.Item == newType) {
                    button.SetBlockedState(false);
                    break;
                }
            }
        }

        public override void SelectButton(BedChoiceButton button) {
            base.SelectButton(button);
            if (_tutorialManager.IsWork)
                _tutorialManager.NextTutorialPart();

            _ingredientsRenderer.ShowIngredients(button.Item);
            _submitButton.interactable &= _ingredientsRenderer.HaveIngredients(button.Item);
        }

        public override void SubmitChoice() {
            base.SubmitChoice();
            if (_tutorialManager.IsWork)
                _tutorialManager.NextTutorialPart();

            _changingBed.SetType(_choosedButton.Item);
            _changingBed = null;
            Disable();
        }

        public override void Disable() {
            base.Disable();
            _changingBed = null;
        }
    }
}
