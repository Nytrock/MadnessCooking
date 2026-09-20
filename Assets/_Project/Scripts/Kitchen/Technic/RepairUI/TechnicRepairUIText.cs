using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    [RequireComponent(typeof(LocalizedText), typeof(TextAvailableRenderer))]
    public class TechnicRepairUIText : MonoBehaviour {
        [SerializeField] private Color _defaultColor;
        [SerializeField] private string _fullStrengthMessage;
        [SerializeField] private string _tutorialMessage;

        private LocalizedText _localizedText;
        private TextAvailableRenderer _textAvailableRenderer;
        private int _repairPrice;

        private void Awake() {
            _localizedText = GetComponent<LocalizedText>();
            _textAvailableRenderer = GetComponent<TextAvailableRenderer>();
        }

        private void SetDefaultMessage(string message) {
            _localizedText.SetColor(_defaultColor);
            _localizedText.SetText(message);
        }

        public void SetPrice(int price, bool isTutorial) {
            if (isTutorial) {
                SetDefaultMessage(_tutorialMessage);
                return;
            }

            if (price == 0) {
                SetDefaultMessage(_fullStrengthMessage);
                return;
            }

            _repairPrice = price;
            UpdatePrice(MoneyManager.Instance.MoneyCount);
            _localizedText.SetText(price.ToString() + '$');
        }

        public void UpdatePrice(int moneyCount) {
            _textAvailableRenderer.UpdateAvailable(_repairPrice <= moneyCount);
        }
    }
}
