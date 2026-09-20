using UnityEngine;
using UnityEngine.UI;

namespace MadnessCooking.General {
    public class MessagePanel : MonoBehaviour {
        [SerializeField] private RectTransform[] _layoutsToRebuild;
        [SerializeField] private GameObject _panelWithBackground;
        [SerializeField] private RectTransform _panel;

        [SerializeField] private LocalizedText _titleText;
        [SerializeField] private LocalizedText _descriptionText;

        [SerializeField] private Button _buttonSubmit;
        [SerializeField] private LocalizedText _buttonSubmitText;

        private void Awake() {
            ChangeState(false);
            _buttonSubmit.onClick.AddListener(delegate { ChangeState(false); });
        }

        public void SetInfo(MessagePanelInfo info) {
            _titleText.SetText(info.Title);
            _descriptionText.SetText(info.Description);
            _panel.localPosition = info.Position;
            _buttonSubmit.gameObject.SetActive(info.IsSubmitButton);
            _buttonSubmitText.SetText(info.Submit);

            ChangeState(true);
            foreach (var layout in _layoutsToRebuild)
                LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
        }

        private void ChangeState(bool newState) {
            _panelWithBackground.SetActive(newState);
        }
    }
}
