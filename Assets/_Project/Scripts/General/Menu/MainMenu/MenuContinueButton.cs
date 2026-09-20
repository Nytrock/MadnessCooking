using UnityEngine;

namespace MadnessCooking.General {
    public class MenuContinueButton : MenuButton {
        [SerializeField] private GameSaveManager _saveManager;

        public override void ChangeSelectVisual(bool newState) {
            _button.interactable = newState && _saveManager.IsDataExists();
        }
    }
}
