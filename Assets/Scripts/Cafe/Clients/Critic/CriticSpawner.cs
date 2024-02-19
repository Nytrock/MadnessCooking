using System;
using System.Collections;
using UnityEngine;

public class CriticSpawner : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private PopularityCalculator _popularityCalculator;
    [SerializeField] private ClientsSpawner _clientSpawner;
    [SerializeField] private CriticUIManager _criticUI;
    private PopularityManager _popularityManager;

    [SerializeField, Min(0)] private float[] _needPopularity;
    private int _nextPopularityIndex = 0;

    private bool _isWaitingCritic;

    private void Awake()
    {
        _popularityManager = _popularityCalculator.GetComponent<PopularityManager>();
    }

    private void Start()
    {
        _timeManager.DaytimeChanged += CheckDaytime;
    }

    private void CheckDaytime(Daytime daytime)
    {
        if (daytime == Daytime.Night && _isWaitingCritic)
            WaitFailure();

        if (daytime == Daytime.Morning)
           if (_needPopularity[_nextPopularityIndex] <= _popularityCalculator.GetPopularity())
               ActivateCriticWait();
    }

    private void ActivateCriticWait()
    {
        _isWaitingCritic = true;

        var morging = _timeManager.GetDaytimeStartInfo(Daytime.Morning);
        var night = _timeManager.GetDaytimeStartInfo(Daytime.Night);

        var hour = UnityEngine.Random.Range(morging.Hour, night.Hour);
        var minute = UnityEngine.Random.Range(morging.Minute, night.Minute);
        var timeCritic = new TimeSpan(hour, minute, 0);
        StartCoroutine(WaitCriticTime(timeCritic));

        _criticUI.ChangeCriticWaitStartUI(true);
    }

    private IEnumerator WaitCriticTime(TimeSpan timeCritic)
    {
        yield return new WaitUntil(() => timeCritic >= _timeManager.TimeSpan);
        _clientSpawner.ChangeCriticWait(true);
    }

    private void DisableCriticWait()
    {
        _isWaitingCritic = false;
        _clientSpawner.ChangeCriticWait(false);
    }


    public void WaitSuccess()
    {
        DisableCriticWait();
        _nextPopularityIndex++;
        _popularityManager.NextLevel();
        _criticUI.ChangeCriticWaitSuccessUI(true);
    }
    
    public void WaitFailure()
    {
        DisableCriticWait();
        _popularityManager.PreviousLevel();
        _criticUI.ChangeCriticWaitFailureUI(true);
    }
}
