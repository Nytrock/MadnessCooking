using UnityEngine;
using UnityEngine.UI;

public class PopularityUIRenderer : MonoBehaviour {
    [SerializeField] private Image _panel;
    [SerializeField] private Sprite[] _panelSprites;
    [SerializeField] private Image _levelJewels;
    [SerializeField] private Sprite[] _levelJewelsSprites;

    public void UpdateInfo(PopularityLevel level) {
        if (_panelSprites.Length <= level.Number / 5)
            return;

        if (_levelJewelsSprites.Length <= level.Number % 5)
            return;

        _panel.sprite = _panelSprites[level.Number / 5];
        _levelJewels.sprite = _levelJewelsSprites[level.Number % 5];
    }
}
