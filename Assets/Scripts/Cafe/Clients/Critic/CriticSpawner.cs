using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CriticSpawner : MonoBehaviour {
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private PopularityCalculator _popularityCalculator;
    [SerializeField] private ClientsSpawner _clientSpawner;
    [SerializeField] private CriticUIManager _criticUI;
    private PopularityManager _popularityManager;

    [SerializeField, Min(0)] private float[] _needPopularity;
    private int _nextPopularityIndex = 0;

    private bool _isWaitingCritic;

    private void Awake() {
        _popularityManager = _popularityCalculator.GetComponent<PopularityManager>();
        _timeManager.DaytimeChanged += CheckDaytime;
    }

    private void CheckDaytime(Daytime daytime) {
        if (daytime == Daytime.Night && _isWaitingCritic)
            WaitFailure();

        if (daytime == Daytime.Morning)
            if (_needPopularity[_nextPopularityIndex] <= _popularityCalculator.GetPopularity())
                ActivateCriticWait();
    }

    private void ActivateCriticWait() {
        _isWaitingCritic = true;

        DaytimeStart morging = _timeManager.GetDaytimeStartInfo(Daytime.Morning);
        DaytimeStart night = _timeManager.GetDaytimeStartInfo(Daytime.Night);

        int hour = Random.Range(morging.Hour, night.Hour);
        int minute = Random.Range(morging.Minute, night.Minute);
        TimeSpan timeCritic = new(hour, minute, 0);
        StartCoroutine(WaitCriticTime(timeCritic));

        _criticUI.ChangeCriticWaitStartUI(true);
    }

    private IEnumerator WaitCriticTime(TimeSpan timeCritic) {
        yield return new WaitUntil(() => timeCritic >= _timeManager.TimeSpan);
        _clientSpawner.ChangeCriticWait(true);
    }

    private void DisableCriticWait() {
        _isWaitingCritic = false;
        _clientSpawner.ChangeCriticWait(false);
    }


    public void WaitSuccess() {
        DisableCriticWait();
        _nextPopularityIndex++;
        _popularityManager.NextLevel();
        _criticUI.ChangeCriticWaitSuccessUI(true);
    }

    public void WaitFailure() {
        DisableCriticWait();
        _popularityManager.PreviousLevel();
        _criticUI.ChangeCriticWaitFailureUI(true);
    }
}
