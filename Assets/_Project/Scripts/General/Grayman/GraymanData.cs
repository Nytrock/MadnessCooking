using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class GraymanData {
        [SerializeField, JsonProperty] private bool _heWasHere;

        public bool HeWasHere => _heWasHere;

        public void HeVisitedUs() {
            _heWasHere = true;
        }
    }
}
