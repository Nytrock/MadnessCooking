public class WaterBedWater : StandardBedWater {
    public override void SetData(BedHolderBoosterData data) {
        base.SetData(data);
        _data.SetBoost(0);
    }
}
