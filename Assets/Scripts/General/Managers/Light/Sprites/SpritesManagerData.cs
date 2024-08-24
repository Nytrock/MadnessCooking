using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class SpritesManagerData : SubLightManagerData {
    [SerializeField, JsonProperty] private JsonColor _previousLight;
    [SerializeField, JsonProperty] private JsonColor _nowLight;
    [SerializeField, JsonProperty] private JsonColor _targetLight;

    public Color NowLight => _nowLight.GetColor();

    public override void UpdateMaterial(float nowTime) {
        _nowLight = new(Color.Lerp(_previousLight.GetColor(), _targetLight.GetColor(), nowTime));
    }

    public override void SetInitialLight(DaytimeLight daytimeLight) {
        var daytimeSprites = daytimeLight as DaytimeSprites;

        _nowLight = new(daytimeSprites.SpriteColor);
        _targetLight = new(daytimeSprites.SpriteColor);
    }

    public override void SetNewLight(DaytimeLight daytimeLight) {
        var daytimeSprites = daytimeLight as DaytimeSprites;
        _previousLight = _targetLight;
        _targetLight = new(daytimeSprites.SpriteColor);
    }
}
