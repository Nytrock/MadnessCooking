using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class LocationPoint {
        [SerializeField] private Location _location;
        [SerializeField] private Transform _point;
        [SerializeField, Min(0)] private float _fatigueCoef;
        [SerializeField] private bool _isHideUI;

        public Location Location => _location;
        public Vector3 Point => new(_point.position.x, _point.position.y, -10);
        public float FatigueCoef => _fatigueCoef;
        public bool IsHideUI => _isHideUI;
    }
}
