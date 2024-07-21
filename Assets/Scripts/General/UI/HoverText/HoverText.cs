using UnityEngine;
using UnityEngine.UI;

public class HoverText : MonoBehaviour {
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

        transform.position = Input.mousePosition;
    }

    public void ShowText(string text) {
        _text.SetText(text);
        ChangeState(true);

        float mousePosition = Input.mousePosition.x;
        Direction panelDirection = Direction.Right;
        if (mousePosition < Screen.width / 2)
            panelDirection = Direction.Left;

        LayoutRebuilder.ForceRebuildLayoutImmediate(_panel);
        _panel.localPosition = panelDirection.ToFloat() * (_offset + new Vector2(_panel.sizeDelta.x / 2, 0));
    }

    public void ChangeState(bool newState) {
        _isHovered = newState;
        _panel.gameObject.SetActive(newState);
    }
}
