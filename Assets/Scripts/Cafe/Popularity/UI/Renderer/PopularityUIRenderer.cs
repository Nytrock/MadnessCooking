using TMPro;
using UnityEngine;

public class PopularityUIRenderer : MonoBehaviour {
    [SerializeField] private PopularityUIPart _mainPanel;
    [SerializeField] private PopularityUIPart _morePanel;
    [SerializeField] private PopularityUIPart _levelCrystals;
    [SerializeField] private TextMeshProUGUI[] _texts;
    [SerializeField] private Color[] _textColors;

    public void UpdateInfo(PopularityLevel level) {
        int number = level.Number - 1;
        _mainPanel.SetSprite(number / 5);
        _morePanel.SetSprite(number / 5);
        _levelCrystals.SetSprite(number % 5);

        foreach (var text in _texts)
            text.color = _textColors[number / 5];
    }
}
