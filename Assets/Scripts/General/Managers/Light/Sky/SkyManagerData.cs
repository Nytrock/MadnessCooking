using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class SkyManagerData : SubLightManagerData {
    [SerializeField, JsonProperty] private JsonColor _nowTopColor;
    [SerializeField, JsonProperty] private JsonColor _nowBottomColor;

    [SerializeField, JsonProperty] private JsonColor _previousTopColor;
    [SerializeField, JsonProperty] private JsonColor _previousBottomColor;

    [SerializeField, JsonProperty] private JsonColor _targetTopColor;
    [SerializeField, JsonProperty] private JsonColor _targetBottomColor;

    public Color NowTopColor => _nowTopColor.GetColor();
    public Color NowBottomColor => _nowBottomColor.GetColor();

    public override void UpdateMaterial(float nowTime) {
        _nowTopColor = new(Color.Lerp(_previousTopColor.GetColor(), _targetTopColor.GetColor(), nowTime));
        _nowBottomColor = new(Color.Lerp(_previousBottomColor.GetColor(), _targetBottomColor.GetColor(), nowTime));
    }

    public override void SetInitialLight(DaytimeLight daytimeLight) {
        var daytimeSky = daytimeLight as DaytimeSky;

        _nowTopColor = new(daytimeSky.SkyGradient.Evaluate(0));
        _nowBottomColor = new(daytimeSky.SkyGradient.Evaluate(1));
        _targetTopColor = new(daytimeSky.SkyGradient.Evaluate(0));
        _targetBottomColor = new(daytimeSky.SkyGradient.Evaluate(1));
    }

    public override void SetNewLight(DaytimeLight daytimeLight) {
        var daytimeSky = daytimeLight as DaytimeSky;

        _previousTopColor = _targetTopColor;
        _previousBottomColor = _targetBottomColor;
        _targetTopColor = new(daytimeSky.SkyGradient.Evaluate(0));
        _targetBottomColor = new(daytimeSky.SkyGradient.Evaluate(1));
    }
}
