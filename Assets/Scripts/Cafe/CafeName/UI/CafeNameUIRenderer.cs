using TMPro;
using UnityEngine;

public class CafeNameUIRenderer : MonoBehaviour {
    [SerializeField] private PopularityManager _popularityManager;
    [SerializeField] private PopularityLevelImage[] _images;
    [SerializeField] private PopularityLevelImage[] _panels;
    [SerializeField] private TextMeshProUGUI[] _texts;
    [SerializeField] private Color[] _textColors;

    private void Awake() {
        _popularityManager.LevelChanged += UpdateVisual;
    }

    private void UpdateVisual(PopularityLevel level) {
        int number = level.Number + 1;

        foreach (var image in _images)
            image.SetSprite(number);

        foreach (var text in _texts)
            text.color = _textColors[number / 5];
    }
}
