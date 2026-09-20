using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class SkyManagerData : SubLightManagerData {
        [SerializeField, JsonProperty] private Color _nowTopColor;
        [SerializeField, JsonProperty] private Color _nowBottomColor;

        [SerializeField, JsonProperty] private Color _previousTopColor;
        [SerializeField, JsonProperty] private Color _previousBottomColor;

        [SerializeField, JsonProperty] private Color _targetTopColor;
        [SerializeField, JsonProperty] private Color _targetBottomColor;

        public Color NowTopColor => _nowTopColor;
        public Color NowBottomColor => _nowBottomColor;

        public override void UpdateMaterial(float nowTime) {
            _nowTopColor = Color.Lerp(_previousTopColor, _targetTopColor, nowTime);
            _nowBottomColor = Color.Lerp(_previousBottomColor, _targetBottomColor, nowTime);
        }

        public override void SetInitialLight(DaytimeLight daytimeLight) {
            var daytimeSky = daytimeLight as DaytimeSky;

            _nowTopColor = daytimeSky.SkyGradient.Evaluate(0);
            _nowBottomColor = daytimeSky.SkyGradient.Evaluate(1);
            _targetTopColor = daytimeSky.SkyGradient.Evaluate(0);
            _targetBottomColor = daytimeSky.SkyGradient.Evaluate(1);
        }

        public override void SetNewLight(DaytimeLight daytimeLight) {
            var daytimeSky = daytimeLight as DaytimeSky;

            _previousTopColor = _targetTopColor;
            _previousBottomColor = _targetBottomColor;
            _targetTopColor = daytimeSky.SkyGradient.Evaluate(0);
            _targetBottomColor = daytimeSky.SkyGradient.Evaluate(1);
        }
    }
}
