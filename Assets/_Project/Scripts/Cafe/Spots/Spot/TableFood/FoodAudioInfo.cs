using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    [Serializable]
    public class FoodAudioInfo : AudioInfo {
        [SerializeField] private FoodType _foodType;

        public FoodType FoodType => _foodType;
    }
}
