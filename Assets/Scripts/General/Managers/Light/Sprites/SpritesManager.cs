using UnityEngine;

public class SpritesManager : SubLightManager {
    [SerializeField] private DaytimeSprites[] _lights;
    protected SpritesManagerData _specialData;

    public override void UpdateMaterial() {
        _material.SetColor("_LightColor", _specialData.NowLight);
    }

    protected override DaytimeLight FindLightByDaytime(Daytime daytime) {
        foreach (var light in _lights)
            if (light.Daytime == daytime)
                return light;
        return null;
    }

    public override void Bind(ref SubLightManagerData data, bool isFileEmpty) {
        if (isFileEmpty)
            data = new SpritesManagerData();
        _data = data;
        _specialData = _data as SpritesManagerData;
    }
}
