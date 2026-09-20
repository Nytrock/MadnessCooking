using UnityEngine;
using UnityEngine.UI;
using MadnessCooking.General;

namespace MadnessCooking.Office {
    public class InternetHomePage : InternetPage {
        [SerializeField] private Button _homeButton;

        public override void ChangeState(bool newValue) {
            base.ChangeState(newValue);
            _homeButton.interactable = !newValue;
        }
    }
}
