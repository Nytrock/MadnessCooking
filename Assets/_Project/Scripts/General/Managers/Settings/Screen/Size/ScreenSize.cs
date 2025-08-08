using System;
using UnityEngine;

[Serializable]
public class ScreenSize {
    [SerializeField, Min(1)] private int _width;
    [SerializeField, Min(1)] private int _heigth;

    public int Height => _heigth;
    public int Width => _width;

    public ScreenSize(int width, int heigth) {
        _width = width;
        _heigth = heigth;
    }

    public override string ToString() {
        return _width.ToString() + " x " + _heigth.ToString();
    }
}
