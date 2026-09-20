using Newtonsoft.Json;
using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Office {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class JokesData {
        [SerializeField, JsonProperty] private bool _isAnis;

        public bool IsAnis => _isAnis;

        public void ChangeAnisState(bool isAnis) {
            _isAnis = isAnis;
        }
    }
}
