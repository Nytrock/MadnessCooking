public class WaterBedWater : StandardBedWater
{
    public override void EndBoost()
    {
        _standardSpeed = 0;
        base.EndBoost();
    }
}
