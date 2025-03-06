using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class SpritesManagerData : SubLightManagerData {
    [SerializeField, JsonProperty] private Color _previousLight;
    [SerializeField, JsonProperty] private Color _nowLight;
    [SerializeField, JsonProperty] private Color _targetLight;

    public Color NowLight => _nowLight;

    public override void UpdateMaterial(float nowTime) {
        _nowLight = Color.Lerp(_previousLight, _targetLight, nowTime);
    }

    public override void SetInitialLight(DaytimeLight daytimeLight) {
        var daytimeSprites = daytimeLight as DaytimeSprites;

        _nowLight = daytimeSprites.SpriteColor;
        _targetLight = daytimeSprites.SpriteColor;
    }

    public override void SetNewLight(DaytimeLight daytimeLight) {
        var daytimeSprites = daytimeLight as DaytimeSprites;
        _previousLight = _targetLight;
        _targetLight = daytimeSprites.SpriteColor;
    }
}
