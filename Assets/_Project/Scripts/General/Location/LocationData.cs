using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class LocationData {
        [SerializeField] private Location _location;
        [SerializeField] private Sprite _sprite;

        public Location Location => _location;
        public Sprite Sprite => _sprite;
        public string Name => "Location." + _location.ToString();
    }
}
