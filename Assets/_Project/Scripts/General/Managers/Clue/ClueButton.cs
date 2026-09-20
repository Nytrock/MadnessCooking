using UnityEngine;
using UnityEngine.UI;

namespace MadnessCooking.General {
    [RequireComponent(typeof(Button))]
    public class ClueButton : MonoBehaviour {
        [SerializeField] private ClueManager _manager;
        private Button _button;

        private void Awake() {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(_manager.HideClue);
        }
    }
}
