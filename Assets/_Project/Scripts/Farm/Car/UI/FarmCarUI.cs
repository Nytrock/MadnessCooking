using UnityEngine;
using UnityEngine.UI;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class FarmCarUI : IngredientStorageUI {
        [SerializeField] private FarmCarWaitManager _waitManager;
        [SerializeField] private Button _sendButton;

        protected override void Awake() {
            if (_sendButton != null)
                _sendButton.onClick.AddListener(_waitManager.Send);
            _waitManager.StateChanged += UpdateState;
            base.Awake();
        }

        private void UpdateState(CarState newState) {
            if (newState != CarState.Returns)
                _panel.SetActive(false);

            if (newState == CarState.Sent)
                CarLeave();

            if (_sendButton != null)
                _sendButton.interactable = newState == CarState.Calm;
        }

        private void CarLeave() {
            foreach (var button in _buttons)
                _buttonPool.PutObject(button);
            _buttons.Clear();
        }
    }
}
