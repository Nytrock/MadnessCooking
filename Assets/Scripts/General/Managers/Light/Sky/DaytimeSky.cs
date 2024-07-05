using System;
using UnityEngine;

[Serializable]
public class DaytimeSky : DaytimeLight {
    [SerializeField] private Gradient _skyGradient;

    public Gradient SkyGradient => _skyGradient;
}
