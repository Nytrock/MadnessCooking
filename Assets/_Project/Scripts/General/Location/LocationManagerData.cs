using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class LocationManagerData {
        [SerializeField, JsonProperty] private Location _location;

        public Location Location => _location;

        public LocationManagerData() {
            _location = Location.Cafe;
        }

        public void ChangeLocation(Location newLocation) {
            _location = newLocation;
        }
    }
}