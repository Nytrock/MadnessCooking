using System;
using System.Collections.Generic;
using UnityEngine;

public class PestsGenerator : MonoBehaviour
{
    [SerializeField] private int _maxPests;
    [SerializeField] private PestsPool _pool;
    [SerializeField] private float _onePestSlowdown;

    [Header("Time settings")]
    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;

    private float _nowTime;
    private float _needTime;

    private readonly List<Pest> _pests = new List<Pest>();
    private bool _isActive;

    public event Action<float> PestsChanged;

    private void Update()
    {
        if (!_isActive)
            return;

        if (_nowTime < _needTime) {
            _nowTime += Time.deltaTime;
        } else {
            SpawnPest();
            _nowTime = 0;
            _needTime = UnityEngine.Random.Range(_minTime, _maxTime);
        }
    }

    private void SpawnPest()
    {
        var pest = _pool.GetObject();
        _pests.Add(pest);
        CheckWork();
    }

    public void ChangeMode(bool newMode)
    {
        _isActive = newMode;
        if (!newMode) {
            CleanPests();
        } else {
            _needTime = UnityEngine.Random.Range(_minTime, _maxTime);
            _nowTime = 0;
        }
    }

    private void CleanPests()
    {
        foreach (var pest in _pests)
            _pool.PutObject(pest);
    }

    public List<Pest> GetList()
    {
        List<Pest> result = new List<Pest>();
        foreach (var pest in _pests)
            result.Add(pest);
        return result;
    }

    public void RemovePest(Pest pest)
    {
        _pool.PutObject(pest);
        _pests.Remove(pest);
        CheckWork();
    }

    private void CheckWork()
    {
        if (_pests.Count == _maxPests) {
            PestsChanged?.Invoke(0);
            _isActive = false;
        } else {
            _isActive = true;
            PestsChanged?.Invoke(1 - _pests.Count * _onePestSlowdown);
        }
    }
}
