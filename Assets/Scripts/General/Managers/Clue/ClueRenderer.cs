using UnityEngine;
using UnityEngine.UI;

public class ClueRenderer : MonoBehaviour {
    [SerializeField] private RectTransform _panel;
    [SerializeField] private LocalizedText _messageText;
    [SerializeField] private Button _button;
    [SerializeField] private RectTransform[] _layoutsToRebuild;

    public void RenderClue(ClueTemplate clue) {
        _panel.position = clue.RectTransform.position;
        _messageText.SetText(clue.Message);
        _button.gameObject.SetActive(clue.IsButtonVisible);

        foreach (var layout in _layoutsToRebuild)
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
    }
}
