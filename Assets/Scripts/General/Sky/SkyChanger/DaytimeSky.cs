using System;
using UnityEngine;

[Serializable]
public class DaytimeSky {
    [SerializeField] private Gradient _skyGradient;
    [SerializeField] private Daytime _daytime;

    public Gradient SkyGradient => _skyGradient;
    public Daytime Daytime => _daytime;
}
