using System;
using UnityEngine;
using UnityEngine.UI;
using MadnessCooking.General;

namespace MadnessCooking.Office {
    public class InternetDownloadRenderer : MonoBehaviour {
        [SerializeField] private InternetDownloadStyle _searchStyle;
        [SerializeField] private InternetDownloadStyle[] _shopStyles;

        [SerializeField] private Image _background;
        [SerializeField] private Image _sliderBackground;
        [SerializeField] private GradientSlider _slider;
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
                throw new ArgumentNullException($"No download style for {page.name}");
            }
        }

        private void SetStyle(InternetDownloadStyle style) {
            _background.color = style.BackgroundColor;
            _slider.SetGradient(style.SliderGradient);
            _sliderBackground.sprite = style.SliderSprite;
            _downloadText.SetColor(style.TextColor);
        }
    }
}
