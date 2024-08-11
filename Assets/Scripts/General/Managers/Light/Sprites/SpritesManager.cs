using UnityEngine;

public class SpritesManager : SubLightManager {
    [SerializeField] private Material _textMaterial;
    [SerializeField] private DaytimeSprites[] _lights;
    [SerializeField] protected SpritesManagerData _specialData;

    public override void UpdateMaterial() {
        _material.SetColor("_LightColor", _specialData.NowLight);
        _textMaterial.SetColor("_FaceColor", _specialData.NowLight);
    }

    protected override DaytimeLight FindLightByDaytime(Daytime daytime) {
        foreach (var light in _lights)
            if (light.Daytime == daytime)
                return light;
        return null;
    }

    public override void Bind(SubLightManagerData data) {
        _data = data;
        _specialData = _data as SpritesManagerData;
    }
}
