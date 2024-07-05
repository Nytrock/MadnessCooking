using System;
using UnityEngine;

[Serializable]
public class SpritesManagerData : SubLightManagerData {
    [SerializeField] private Color _previousLight;
    [SerializeField] private Color _nowLight;
    [SerializeField] private Color _targetLight;

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
