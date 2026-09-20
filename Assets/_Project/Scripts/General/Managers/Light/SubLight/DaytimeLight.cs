using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public abstract class DaytimeLight {
        [SerializeField] private Daytime _daytime;

        public Daytime Daytime => _daytime;
    }
}
