using System;
using UnityEngine;

[Serializable]
public abstract class DaytimeLight {
    [SerializeField] private Daytime _daytime;

    public Daytime Daytime => _daytime;
}
