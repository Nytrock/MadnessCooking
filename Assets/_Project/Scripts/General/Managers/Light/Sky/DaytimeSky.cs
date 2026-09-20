using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class DaytimeSky : DaytimeLight {
        [SerializeField] private Gradient _skyGradient;

        public Gradient SkyGradient => _skyGradient;
    }
}
