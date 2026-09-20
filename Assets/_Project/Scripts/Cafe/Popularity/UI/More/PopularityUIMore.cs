using TMPro;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class PopularityUIMore : MonoBehaviour {
        [SerializeField] private GameObject _panel;
        [SerializeField] private GameObject _criticWaitText;
        [SerializeField] private LocalizedText _descriptionText;
        [SerializeField] private TextMeshProUGUI _singleChanceText;
        [SerializeField] private TextMeshProUGUI _doubleChanceText;
        [SerializeField] private TextMeshProUGUI _tripleChanceText;
        [SerializeField] private TextMeshProUGUI _quarterChanceText;

        private void Start() {
            ChangeMode(false);
        }

        public void ChangeMode(bool newMode) {
            _panel.SetActive(newMode);
        }

        public void UpdateInfo(PopularityLevel level) {
            _descriptionText.SetText(level.Description);
            _singleChanceText.text = level.SingleChance.ToString() + "%";
            _doubleChanceText.text = level.DoubleChance.ToString() + "%";
            _tripleChanceText.text = level.TripleChance.ToString() + "%";
            _quarterChanceText.text = level.QuarterChance.ToString() + "%";
        }

        public void ChangeCriticWaitText(bool newState) {
            _criticWaitText.SetActive(newState);
        }
    }
}
