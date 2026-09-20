using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class RealTimeManagerData {
        [SerializeField, JsonProperty] private float _realTime;

        public RealTimeManagerData() {
            _realTime = 0;
        }

        public void UpdateRealTime() {
            _realTime += Time.unscaledDeltaTime;
        }
    }
}
