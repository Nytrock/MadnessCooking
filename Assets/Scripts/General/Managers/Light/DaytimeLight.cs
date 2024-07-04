using System;
using UnityEngine;

[Serializable]
public class DaytimeLight {
    [SerializeField] private Daytime _daytime;
    [SerializeField] private Color _lightColor;

    public Daytime Daytime => _daytime;
    public Color LightColor => _lightColor;
}
