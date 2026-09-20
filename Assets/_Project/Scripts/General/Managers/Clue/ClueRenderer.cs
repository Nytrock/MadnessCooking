using UnityEngine;
using UnityEngine.UI;

namespace MadnessCooking.General {
    public class ClueRenderer : MonoBehaviour {
        [SerializeField] private RectTransform _panel;
        [SerializeField] private LocalizedText _messageText;
        [SerializeField] private Button _button;
        [SerializeField] private LocalizedText _buttonText;

        private ClueTemplate _template;
        private RectTransform _buttonRect;

        private void Awake() {
            _buttonRect = _button.GetComponent<RectTransform>();
            _messageText.TextChanged += UpdatePosition;
        }

        public void StartRenderClue(ClueTemplate clue) {
            _template = clue;

            _messageText.SetText(_template.Message);
            _button.gameObject.SetActive(_template.IsButtonVisible);
            _buttonText.SetText(_template.ButtonMessage);
        }

        private void UpdatePosition() {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_panel);

            Vector2 startPosition = _template.RectTransform.localPosition;
            CluePositionMode positionMode = _template.PositionMode;
            Vector2 holeSize = _template.RectTransform.sizeDelta;
            holeSize = new Vector2(holeSize.x / 2, holeSize.y / 2);

            float offset = _template.PositionOffset;
            float panelWidth = _panel.sizeDelta.x / 2;
            float panelHeight = _panel.sizeDelta.y / 2;
            if (_template.IsButtonVisible)
                panelHeight += _buttonRect.sizeDelta.y / 2;

            switch (positionMode) {
                case CluePositionMode.UpperLeft:
                    startPosition += new Vector2(-holeSize.x, holeSize.y);
                    startPosition += new Vector2(-panelWidth, panelHeight);
                    startPosition += new Vector2(-offset, offset);
                    break;
                case CluePositionMode.UpperCenter:
                    startPosition += new Vector2(0, holeSize.y);
                    startPosition += new Vector2(0, panelHeight);
                    startPosition += new Vector2(0, offset);
                    break;
                case CluePositionMode.UpperRight:
                    startPosition += new Vector2(holeSize.x, holeSize.y);
                    startPosition += new Vector2(panelWidth, panelHeight);
                    startPosition += new Vector2(offset, offset);
                    break;
                case CluePositionMode.MiddleLeft:
                    startPosition += new Vector2(-holeSize.x, 0);
                    startPosition += new Vector2(-panelWidth, 0);
                    startPosition += new Vector2(-offset, 0);
                    break;
                case CluePositionMode.MiddleCenter:
                    break;
                case CluePositionMode.MiddleRight:
                    startPosition += new Vector2(holeSize.x, 0);
                    startPosition += new Vector2(panelWidth, 0);
                    startPosition += new Vector2(offset, 0);
                    break;
                case CluePositionMode.LowerLeft:
                    startPosition += new Vector2(-holeSize.x, -holeSize.y);
                    startPosition += new Vector2(-panelWidth, -panelHeight);
                    startPosition += new Vector2(-offset, -offset);
                    break;
                case CluePositionMode.LowerCenter:
                    startPosition += new Vector2(0, -holeSize.y);
                    startPosition += new Vector2(0, -panelHeight);
                    startPosition += new Vector2(0, -offset);
                    break;
                case CluePositionMode.LowerRight:
                    startPosition += new Vector2(holeSize.x, -holeSize.y);
                    startPosition += new Vector2(panelWidth, -panelHeight);
                    startPosition += new Vector2(offset, -offset);
                    break;
            }

            _panel.localPosition = startPosition;
            if (_template.IsButtonVisible) {
                Vector2 buttonPosition = startPosition;
                buttonPosition -= new Vector2(0, panelHeight - 8.3f);
                _buttonRect.localPosition = buttonPosition;
            }
        }
    }
}