using UnityEngine;

[RequireComponent(typeof(TimeManager))]
public class InGameTime : Singleton<InGameTime> {
    private TimeManager _timeManager;

    public float DeltaTime => _timeManager.NowTimeSpeed * Time.deltaTime;

    protected override void Awake() {
        base.Awake();
        _timeManager = GetComponent<TimeManager>();
    }


}
