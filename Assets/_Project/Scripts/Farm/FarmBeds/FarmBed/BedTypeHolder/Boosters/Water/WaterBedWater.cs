public class WaterBedWater : StandardBedWater {
    public override void Activate() {
        base.Activate();
        _data.SetBoost(0);
    }
}
