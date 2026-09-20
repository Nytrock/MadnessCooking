namespace MadnessCooking.General {
    public class SunRotater : AstronomicalObjectsRotater {
        protected override Daytime _startDaytime => Daytime.Morning;
        protected override Daytime _endDaytime => Daytime.Night;
    }
}
