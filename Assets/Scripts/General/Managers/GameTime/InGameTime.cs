using UnityEngine;

[RequireComponent(typeof(GameTimeManager))]
public class InGameTime : Singleton<InGameTime> {
    private GameTimeManager _timeManager;

    public float NormalizedDeltaTime => _timeManager.NormalizedNowTimeSpeed * Time.deltaTime;
    public float DeltaTime => _timeManager.NowTimeSpeed * Time.deltaTime;

    protected override void Awake() {
        base.Awake();
        _timeManager = GetComponent<GameTimeManager>();
    }
}
