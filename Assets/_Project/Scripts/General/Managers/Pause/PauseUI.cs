using UnityEngine;

namespace MadnessCooking.General {
    public class PauseUI : MonoBehaviour {
        [SerializeField] private PauseManager _manager;
        [SerializeField] private GameObject _panel;
        [SerializeField] private MenuButtonSelector _menuSelector;

        private void Awake() {
            _manager.PauseChanged += ChangeState;
        }

        private void ChangeState(bool newState) {
            _panel.SetActive(newState);
            if (newState)
                _menuSelector.SelectFirstButton();
        }
    }
}
