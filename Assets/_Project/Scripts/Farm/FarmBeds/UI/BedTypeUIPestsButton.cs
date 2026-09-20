using UnityEngine;
using UnityEngine.UI;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    [RequireComponent(typeof(Button))]
    public class BedTypeUIPestsButton : MonoBehaviour {
        [SerializeField] private Sprite _defaultIcon;
        [SerializeField] private Sprite _instantPestsIcon;

        private Button _button;

        public void UpdateState(PestsGeneratorData data) {
            _button.interactable = !data.IsPestsRemoved;

            if (data.IsPestsInstant && !data.IsPestsRemoved)
                _button.image.sprite = _instantPestsIcon;
            else
                _button.image.sprite = _defaultIcon;
        }

        private void Awake() {
            _button = GetComponent<Button>();
        }


    }
}
