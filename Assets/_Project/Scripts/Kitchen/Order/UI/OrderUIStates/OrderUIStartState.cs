using UnityEngine;
using UnityEngine.UI;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    public class OrderUIStartState : OrderUIBaseState {
        [SerializeField] private Button _cookButton;

        public void UpdateCookButton(bool newState) {
            _cookButton.interactable = newState;
        }
    }
}
