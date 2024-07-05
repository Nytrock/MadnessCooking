using System;

[Serializable]
public abstract class SubLightManagerData {
    public abstract void UpdateMaterial(float nowTime);
    public abstract void SetInitialLight(DaytimeLight daytimeLight);
    public abstract void SetNewLight(DaytimeLight daytimeLight);
}
