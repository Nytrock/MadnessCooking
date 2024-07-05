using UnityEngine;

public class SkyManager : SubLightManager {
    [SerializeField] private DaytimeSky[] _skyes;
    [SerializeField] protected SkyManagerData _specialData;

    public override void UpdateMaterial() {
        _material.SetColor("_TopColor", _specialData.NowTopColor);
        _material.SetColor("_BottomColor", _specialData.NowBottomColor);
    }

    protected override DaytimeLight FindLightByDaytime(Daytime daytime) {
        foreach (var sky in _skyes)
            if (sky.Daytime == daytime)
                return sky;
        return null;
    }

    public override void Bind(ref SubLightManagerData data, bool isFileEmpty) {
        if (isFileEmpty)
            data = new SkyManagerData();
        _data = data;
        _specialData = _data as SkyManagerData;
    }
}
