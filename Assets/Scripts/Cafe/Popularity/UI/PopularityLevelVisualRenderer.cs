using TMPro;
using UnityEngine;

public class PopularityLevelVisualRenderer : MonoBehaviour {
    [SerializeField] private PopularityManager _popularityManager;
    [SerializeField] private PopularityLevelImage[] _panels;
    [SerializeField] private TextMeshProUGUI[] _texts;
    [SerializeField] private Color[] _textColors;

    private void Awake() {
        _popularityManager.LevelChanged += UpdateInfo;
    }

    public void UpdateInfo(PopularityLevel level) {
        foreach (var image in _panels)
            image.SetSprite(level.Number);

        foreach (var text in _texts)
            text.color = _textColors[level.Number / 5];
    }
}
