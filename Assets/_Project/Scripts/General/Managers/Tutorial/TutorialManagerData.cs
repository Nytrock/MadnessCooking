using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class TutorialManagerData {
        [SerializeField, JsonProperty] private bool _isWork;

        public bool IsWork => _isWork;

        public void ChangeWorkState(bool isWork) {
            _isWork = isWork;
        }
    }
}
