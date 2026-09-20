using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    [RequireComponent(typeof(LocalizedText))]
    public class ChickensUIText : MonoBehaviour {
        [SerializeField] private string _normalFoodText;
        [SerializeField] private string _infiniteFoodText;
        private LocalizedText _text;

        private void CheckText() {
            if (_text != null) return;

            _text = GetComponent<LocalizedText>();
        }

        public void SetInfiniteText() {
            CheckText();
            _text.SetText(_infiniteFoodText);
        }

        public void SetNormalText(int foodCount) {
            CheckText();
            _text.AddArguments("count", foodCount.ToString());
            _text.SetText(_normalFoodText);
        }
    }
}
