using UnityEngine;
using UnityEngine.UI;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class SpotEditorChooseButtons : MonoBehaviour {
        [SerializeField] private Button[] _chooseButtons;

        public void SetButtonsNumber(int freeSpace) {
            for (int i = 0; i < _chooseButtons.Length; i++)
                _chooseButtons[i].interactable = i < freeSpace;
        }
    }
}
