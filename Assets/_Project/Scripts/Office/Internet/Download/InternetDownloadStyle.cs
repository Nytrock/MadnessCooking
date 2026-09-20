using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Office {
    [Serializable]
    public class InternetDownloadStyle {
        [SerializeField] private InternetPage _page;
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Color _textColor;
        [SerializeField] private Gradient _sliderGradient;
        [SerializeField] private Sprite _sliderSprite;

        public InternetPage Page => _page;
        public Color BackgroundColor => _backgroundColor;
        public Color TextColor => _textColor;
        public Gradient SliderGradient => _sliderGradient;
        public Sprite SliderSprite => _sliderSprite;
    }
}
