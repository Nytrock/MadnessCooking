using UnityEngine;

public class RealTimeManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField] private PauseManager _pauseManager;
    [SerializeField] private RealTimeManagerData _data;

    private void Update() {
        if (_pauseManager.IsPause)
            return;

        _data.UpdateRealTime();
    }

    public void LateStart() { }

    public void Bind(GeneralData data) {
        data.RealTimeManager ??= new();
        _data = data.RealTimeManager;
    }
}
