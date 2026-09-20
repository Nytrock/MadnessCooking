using Newtonsoft.Json;
using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class BedHolderBoosterData {
        [SerializeField, JsonProperty] private float _boost;
        [SerializeField, JsonProperty] private bool _isBoosting;
        [SerializeField, JsonProperty] private bool _isEternal;
        [SerializeField, JsonProperty] private float _nowTime;

        public float Boost => _boost;
        public bool IsBoosting => _isBoosting;
        public bool IsEternal => _isEternal;
        public float NowTime => _nowTime;

        public BedHolderBoosterData() {
            _boost = 1;
        }

        public void StartBoost(float boost) {
            _isBoosting = true;
            _nowTime = 0;
            _boost = boost;
        }

        public void EndBoost(float defaultSpeed) {
            _isBoosting = false;
            _boost = defaultSpeed;
        }

        public void UpdateTime() {
            _nowTime += InGameTime.Instance.NormalizedDeltaTime;
        }

        public void SetBoost(int boost) {
            _boost = boost;
        }

        public void BecomeEternal() {
            _isEternal = true;
        }

        public void DisableUpgrades() {
            _isEternal = false;
        }
    }
}
