using System;
using UnityEngine;

[Serializable]
public class BlurSettings {
    [SerializeField, Range(0, 0.6f)] private float _horizontalBlur;
    [SerializeField, Range(0, 0.4f)] private float _verticalBlur;

    public float HorizontalBlur => _horizontalBlur;
    public float VerticalBlur => _verticalBlur;
}
