using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class SpaceManagerData {
        [SerializeField, JsonProperty] private int _count;

        public int Count => _count;

        public SpaceManagerData(int defaultSpaceCount) {
            _count = defaultSpaceCount;
        }

        public void SetCount(CountUpgrade countUpgrade) {
            _count = countUpgrade.Count;
        }
    }
}
