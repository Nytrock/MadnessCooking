using Newtonsoft.Json;
using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class FarmBedManagerData : SpaceManagerData {
        [SerializeField, JsonProperty] private FarmBedData[] _farmBeds;
        [SerializeField, JsonProperty] private int _plantsCount = 0;

        public FarmBedManagerData(int defaultSpaceCount, int bedsCount) : base(defaultSpaceCount) {
            _farmBeds = new FarmBedData[bedsCount];
            for (int i = 0; i < bedsCount; i++)
                _farmBeds[i] = new();
        }

        public FarmBedData GetBedData(int index) {
            return _farmBeds[index];
        }

        public void AddToPlantCount(int count) {
            if (count < 0) return;

            _plantsCount += count;
        }
    }
}
