public class WaterBedWater : BedHolderBooster {
    public override void Activate() {
        base.Activate();
        _data.SetBoost(0);
    }
}
