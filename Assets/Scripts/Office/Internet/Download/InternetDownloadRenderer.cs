using System;
using UnityEngine;
using UnityEngine.UI;

public class InternetDownloadRenderer : MonoBehaviour {
    [SerializeField] private InternetDownloadStyle _searchStyle;
    [SerializeField] private InternetDownloadStyle[] _shopStyles;

    [SerializeField] private Image _background;
    [SerializeField] private Image _sliderBackground;
    [SerializeField] private Image _sliderFill;
    [SerializeField] private LocalizedText _downloadText;

    public void UpdateVisual(InternetPage page) {
        if (page as InternetSearchPage) {
            SetStyle(_searchStyle);
        } else {
            foreach (var style in _shopStyles) {
                if (style.Page == page) {
                    SetStyle(style);
                    return;
                }
            }
            throw new ArgumentNullException($"No download style for {nameof(page)}");
        }
    }

    private void SetStyle(InternetDownloadStyle style) {
        _background.sprite = style.Background;
        _sliderBackground.sprite = style.SliderBackground;
        _sliderFill.color = style.SliderColor;
        _downloadText.SetColor(style.TextColor);
    }
}
