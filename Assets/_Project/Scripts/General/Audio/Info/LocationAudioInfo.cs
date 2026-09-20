using System;
using System.Linq;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class LocationAudioInfo : AudioInfo {
        [SerializeField] private Location[] _locations;

        public bool ContainsLocation(Location location) {
            return _locations.Contains(location);
        }
    }
}
