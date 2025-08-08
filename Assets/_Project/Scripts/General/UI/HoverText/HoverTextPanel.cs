using UnityEngine;
using UnityEngine.UI;

public class HoverTextPanel : MonoBehaviour {
    [SerializeField] private RectTransform _panel;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private LocalizedText _text;

    private bool _isHovered = false;

    private void Awake() {
        ChangeState(false);
    }

    private void Update() {
        if (!_isHovered)
            return;

        UpdatePosition();
    }

    protected virtual void UpdatePosition() {
        transform.position = Input.mousePosition;
    }

    public void ShowText(string text) {
        _text.SetText(text);
        ChangeState(true);

        bool isRight = Input.mousePosition.x < Screen.width / 2;
        Direction panelDirection = isRight.ToDirection();

        LayoutRebuilder.ForceRebuildLayoutImmediate(_panel);
        _panel.localPosition = panelDirection.ToFloat() * (_offset + new Vector2(_panel.sizeDelta.x / 2, 0));
    }

    public void ChangeState(bool newState) {
        _isHovered = newState;
        _panel.gameObject.SetActive(newState);
    }
}
