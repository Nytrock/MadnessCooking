using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class DaytimeMultiplier {
        [SerializeField] private Daytime _daytime;
        [SerializeField, Min(0)] private float _multiplier;

        public Daytime Daytime => _daytime;
        public float Multiplier => _multiplier;
    }
}
