using UnityEngine;
using Random = UnityEngine.Random;

public class CriticSpawner : MonoBehaviour, IBindable<CafeData> {
    [SerializeField] private GameTimeManager _timeManager;
    [SerializeField] private SleepBed _bed;
    [SerializeField] private PopularityManager _popularityManager;
    [SerializeField] private ClientsSpawner _clientSpawner;
    [SerializeField] private CriticUI _criticUI;

    [SerializeField] private CriticSpawnerData _data;


    private void Awake() {
        _timeManager.DaytimeChanged += CheckDaytime;
    }

    private void Update() {
        _data.Update();
    }

    private void CheckDaytime(Daytime daytime) {
        if (daytime == Daytime.Night && _data.IsCriticCanSpawn)
            WaitFailure();

        if (daytime == Daytime.Morning)
            if (_popularityManager.CheckLevelWaitCritic())
                ActivateCriticWait();
    }

    private void ActivateCriticWait() {
        DaytimeStart morging = _timeManager.GetDaytimeStartInfo(Daytime.Morning);
        DaytimeStart night = _timeManager.GetDaytimeStartInfo(Daytime.Night);

        int maxWaitTime = (night.Hour - 2) * 60 + night.Minute - morging.Hour * 60 - morging.Minute;
        int minutes = Random.Range(0, maxWaitTime);
        _data.StartWait(minutes * 60);

        if (_bed.IsSleep)
            _bed.ChangeSleepState(false);
        _criticUI.SetMessage(CriticMessageType.Start);
    }

    public void WaitSuccess() {
        _popularityManager.CriticSuccess();
        _criticUI.SetMessage(CriticMessageType.Success);
    }

    public void WaitFailure() {
        _popularityManager.CriticFailure();
        _criticUI.SetMessage(CriticMessageType.Failure);
    }

    public void LateStart() { }

    public void Bind(CafeData data) {
        data.CriticSpawner ??= new();
        _data = data.CriticSpawner;
    }
}
