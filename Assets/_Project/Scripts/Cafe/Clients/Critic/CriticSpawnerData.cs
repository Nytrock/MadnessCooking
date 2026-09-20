using Newtonsoft.Json;
using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class CriticSpawnerData {
        [SerializeField, JsonProperty] private bool _isWaitingCritic;
        [SerializeField, JsonProperty] private bool _isCriticCanSpawn;
        [SerializeField, JsonProperty] private float _nowTime;
        [SerializeField, JsonProperty] private float _needTime;

        public bool IsCriticCanSpawn => _isCriticCanSpawn;

        public void ChangeCriticSpawn(bool canSpawn) {
            _isCriticCanSpawn = canSpawn;
        }

        public void StartWait(float needTime) {
            _isWaitingCritic = true;
            _nowTime = 0;
            _needTime = needTime;
        }

        public void Update() {
            if (!_isWaitingCritic)
                return;

            _nowTime += InGameTime.Instance.RawTime * FpsManager.NORMALIZED_DELTA_TIME;
            if (_nowTime > _needTime) {
                _isCriticCanSpawn = true;
                _isWaitingCritic = false;
                _nowTime = 0;
            }
        }
    }
}
