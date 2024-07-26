using System;
using UnityEngine;

[Serializable]
public class ScreenSize {
    [SerializeField, Min(1)] private int _heigth;
    [SerializeField, Min(1)] private int _width;

    public int Height => _heigth;
    public int Width => _width;

    public override string ToString() {
        return _heigth.ToString() + " x " + _width.ToString();
    }
}
