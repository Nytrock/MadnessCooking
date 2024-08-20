using UnityEngine;

public class RealTimeManager : MonoBehaviour, IBindable<GeneralData> {
    private RealTimeManagerData _data;

    private void Update() {
        _data.UpdateRealTime();
    }

    public void Bind(GeneralData data) {
        data.RealTimeManager ??= new();
        _data = data.RealTimeManager;
    }
}
