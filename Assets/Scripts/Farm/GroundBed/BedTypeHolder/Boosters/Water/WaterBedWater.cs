using UnityEngine;

public class WaterBedWater : StandardBedWater
{
    protected override void EndBoost()
    {
        _standardSpeed = 0;
        base.EndBoost();
    }
}
