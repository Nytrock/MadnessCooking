using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class FarmUpgradeData : ISaveable {
        [SerializeField, JsonProperty] private bool _isGrowStatusShow;

        public bool IsGrowStatusShow => _isGrowStatusShow;

        public void ChangeGrowStatusShow() {
            _isGrowStatusShow = true;
        }
    }
}
