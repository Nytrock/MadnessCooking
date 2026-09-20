using Newtonsoft.Json;
using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class ChickenFoodData {
        [SerializeField, JsonProperty] private float _nowTime;
        [SerializeField, JsonProperty] private float _needTime;
        [SerializeField, JsonProperty] private float _foodCoef;

        public bool IsEnded => _nowTime >= _needTime;
        public float FoodCoef => _foodCoef;

        public ChickenFoodData(float basicTime, float foodCoef) {
            _foodCoef = foodCoef;
            _needTime = basicTime * _foodCoef;
        }

        public void AddTime() {
            _nowTime += InGameTime.Instance.NormalizedDeltaTime;
        }
    }
}
