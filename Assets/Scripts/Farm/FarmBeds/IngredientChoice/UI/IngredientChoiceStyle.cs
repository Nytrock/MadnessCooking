using System;
using UnityEngine;

[Serializable]
public class IngredientChoiceStyle {
    [SerializeField] private BedType _bedType;
    [SerializeField] private Sprite _panel;
    [SerializeField] private Sprite _close;
    [SerializeField] private Sprite _button;
    [SerializeField] private Sprite _cell;
    [SerializeField] private Color _textColor;

    public BedType BedType => _bedType;
    public Sprite Panel => _panel;
    public Sprite Close => _close;
    public Sprite Button => _button;
    public Sprite Cell => _cell;
    public Color Color => _textColor;
}
