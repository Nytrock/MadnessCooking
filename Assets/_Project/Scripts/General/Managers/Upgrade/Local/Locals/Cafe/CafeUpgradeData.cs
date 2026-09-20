using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class CafeUpgradeData : ISaveable {
        [SerializeField, JsonProperty] private bool _isEatTimeShow;

        public bool IsEatTimeShow => _isEatTimeShow;

        public void ChangeEatTimeShow(bool isEatTimeShow) {
            _isEatTimeShow = isEatTimeShow;
        }
    }
}
