using System;
using UnityEngine;

[Serializable]
public class InternetDownloadStyle {
    [SerializeField] private InternetPage _page;
    [SerializeField] private Sprite _background;
    [SerializeField] private Color _textColor;
    [SerializeField] private Sprite _sliderBackground;
    [SerializeField] private Color _sliderColor;

    public InternetPage Page => _page;
    public Sprite Background => _background;
    public Color TextColor => _textColor;
    public Sprite SliderBackground => _sliderBackground;
    public Color SliderColor => _sliderColor;
}
