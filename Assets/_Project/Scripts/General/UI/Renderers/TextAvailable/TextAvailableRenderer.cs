using TMPro;
using UnityEngine;

namespace MadnessCooking.General {
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextAvailableRenderer : MonoBehaviour {
        [SerializeField] private Color _availableColor;
        [SerializeField] private Color _notAvailableColor;
        private TextMeshProUGUI _text;

        private void Awake() {
            CheckTextMesh();
        }

        private void CheckTextMesh() {
            if (_text != null) return;

            _text = GetComponent<TextMeshProUGUI>();
        }

        public void UpdateAvailable(bool isAvailable) {
            CheckTextMesh();

            if (isAvailable)
                _text.color = _availableColor;
            else
                _text.color = _notAvailableColor;
        }
    }
}
