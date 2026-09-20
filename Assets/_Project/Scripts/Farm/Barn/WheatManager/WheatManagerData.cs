using Newtonsoft.Json;
using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class WheatManagerData {
        [SerializeField, JsonProperty] private bool _isCowNextWheat;
        [SerializeField, JsonProperty] private bool _isWheatDistributed;

        public bool IsCowNextWheat => _isCowNextWheat;
        public bool IsWheatDistributed => _isWheatDistributed;

        public void ChangeCowNextWheat() {
            _isCowNextWheat = !_isCowNextWheat;
        }

        public void DistributeWheat() {
            _isWheatDistributed = true;
        }
    }
}
