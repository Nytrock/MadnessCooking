using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PestsGenerator : MonoBehaviour
{
    [SerializeField, Min(1)] private int _maxPests;
    [SerializeField] private PestsPool _pool;
    [SerializeField, Min(0)] private float _onePestSlowdown;

    [Header("Time settings")]
    [SerializeField, Min(0)] private float _minTime;
    [SerializeField, Min(0)] private float _maxTime;

    private FarmBedData _bedData;
    private PestsGeneratorData _generatorData => _bedData.PestsGenerator;

    private readonly List<Pest> _pests = new();
    private bool _isPause;

    public event Action PestsChanged;

    private void Update()
    {
        if (!_generatorData.IsActive || _isPause || _generatorData.IsPestsRemoved)
            return;

        if (_generatorData.NowTime < _generatorData.NeedTime) {
            _generatorData.NowTime += TimeManager.Instance.InGameTimeSpeed;
        } else {
            SpawnPest();
            _generatorData.NowTime = 0;
            _generatorData.NeedTime = Random.Range(_minTime, _maxTime);
        }
    }

    public void ChangePause(bool newState)
    {
        _isPause = newState;
    }

    private void SpawnPest()
    {
        Pest pest = _pool.GetObject();
        _pests.Add(pest);
        _generatorData.Pests.Add(pest.PestData);
        CheckWork();
    }

    public void ChangeMode(bool newMode)
    {
        _generatorData.IsActive = newMode;
        if (!newMode) {
            CleanPests();
        } else {
            _generatorData.NeedTime = Random.Range(_minTime, _maxTime);
            _generatorData.NowTime = 0;
        }
    }

    public void CleanPests()
    {
        foreach (var pest in _pests)
            _pool.PutObject(pest);
        _pests.Clear();
        _generatorData.Pests.Clear();

        _bedData.PestsSlowdown = 1;
        PestsChanged?.Invoke();
    }

    public IEnumerable<Pest> Pests()
    {
        foreach (var pest in _pests)
            yield return pest;
        yield break;
    }

    public void RemovePest(Pest pest)
    {
        _pool.PutObject(pest);
        _generatorData.Pests.Remove(pest.PestData);
        _pests.Remove(pest);
        CheckWork();
    }

    private void CheckWork()
    {
        if (_pests.Count == _maxPests) {
            _bedData.PestsSlowdown = 0;
            _generatorData.IsActive = false;
        } else {
            _generatorData.IsActive = true;
            _bedData.PestsSlowdown = 1 - (_pests.Count * _onePestSlowdown);
        }
        PestsChanged?.Invoke();
    }

    public void SetData(FarmBedData bedData)
    {
        _bedData = bedData;
        foreach (var pestData in _generatorData.Pests) {
            Pest pest = _pool.GetObject(pestData.PrefabIndex);
            _pests.Add(pest);
            pest.Bind(pestData);
        }
    }
}
