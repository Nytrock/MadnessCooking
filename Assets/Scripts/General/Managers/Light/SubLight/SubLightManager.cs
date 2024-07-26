using UnityEngine;

public abstract class SubLightManager : MonoBehaviour {
    [SerializeField] protected Material _material;
    protected SubLightManagerData _data;

    public void SetNewLight(Daytime daytime) {
        _data.SetNewLight(FindLightByDaytime(daytime));
        UpdateMaterial();
    }

    public void SetInitialLight(Daytime daytime) {
        _data.SetInitialLight(FindLightByDaytime(daytime));
        UpdateMaterial();
    }

    protected abstract DaytimeLight FindLightByDaytime(Daytime daytime);
    public abstract void UpdateMaterial();
    public abstract void Bind(SubLightManagerData data);
}
