using System;
using UnityEngine;

[Serializable]
public class LocationPoint {
    [SerializeField] private Location _location;
    [SerializeField] private Transform _point;
    [SerializeField, Min(0)] private float _fatigueCoef;
    [SerializeField] private bool _isHideUI;

    public Location Location => _location;
    public Vector2 Point => _point.position;
    public float FatigueCoef => _fatigueCoef;
    public bool IsHideUI => _isHideUI;
}
