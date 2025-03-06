public class WaterBedWater : StandardBedWater {
    public override void Bind(BedHolderBoosterData data) {
        base.Bind(data);
        _data.SetBoost(0);
    }
}
