using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CriticSpawner : MonoBehaviour, IBindable<CafeData> {
    [SerializeField] private GameTimeManager _timeManager;
    [SerializeField] private PopularityManager _popularityManager;
    [SerializeField] private ClientsSpawner _clientSpawner;
    [SerializeField] private CriticUI _criticUI;

    private CriticSpawnerData _data;


    private void Awake() {
        _timeManager.DaytimeChanged += CheckDaytime;
    }

    private void CheckDaytime(Daytime daytime) {
        if (daytime == Daytime.Night && _data.IsWaitingCritic)
            WaitFailure();

        if (daytime == Daytime.Morning)
            if (_popularityManager.CheckLevelWaitCritic())
                ActivateCriticWait();
    }

    private void ActivateCriticWait() {
        DaytimeStart morging = _timeManager.GetDaytimeStartInfo(Daytime.Morning);
        DaytimeStart night = _timeManager.GetDaytimeStartInfo(Daytime.Night);

        int hour = Random.Range(morging.Hour, night.Hour);
        int minute = Random.Range(morging.Minute, night.Minute);
        TimeSpan timeCritic = new(hour, minute, 0);
        StartCoroutine(WaitCriticTime(timeCritic));

        _criticUI.SetMessage(CriticMessageType.Start);
    }

    private IEnumerator WaitCriticTime(TimeSpan timeCritic) {
        yield return new WaitUntil(() => timeCritic >= _timeManager.GlobalTime);
        _data.ChangeCriticWait(true);
    }

    private void DisableCriticWait() {
        _data.ChangeCriticWait(false);
    }


    public void WaitSuccess() {
        DisableCriticWait();
        _popularityManager.NextLevel();
        _criticUI.SetMessage(CriticMessageType.Success);
    }

    public void WaitFailure() {
        DisableCriticWait();
        _popularityManager.CriticFailure();
        _criticUI.SetMessage(CriticMessageType.Failure);
    }

    public void LateStart() { }

    public void Bind(CafeData data) {
        data.CriticSpawner ??= new();
        _data = data.CriticSpawner;
    }
}
