using System;
using System.Collections.Generic;
using UnityEngine;

public class PestsGenerator : MonoBehaviour {
    [SerializeField, Min(1)] private int _maxPests;
    [SerializeField] private PestsPool _pool;
    [SerializeField, Min(0)] private float _onePestSlowdown;
    [SerializeField] private RangeFloat _spawnTime;
    private PestsGeneratorData _data;

    private readonly List<Pest> _pests = new();
    private bool _isPause;

    public event Action PestsChanged;
    public event Action PestsCleaned;

    private void Update() {
        if (!_data.IsActive || _isPause || _data.IsPestsRemoved)
            return;

        _data.UpdateTime();
        if (_data.NowTime > _data.NeedTime) {
            SpawnPest();
            _data.SetNewTime(_spawnTime.RandomValue);
        }
    }

    public void ChangePause(bool newState) {
        _isPause = newState;
    }

    private void SpawnPest() {
        Pest pest = _pool.GetObject();
        _pests.Add(pest);
        _data.AddPest(pest.Data);
        CheckSlowdown();
    }

    public void ChangeMode(bool newMode) {
        _data.ChangeMode(newMode);
        if (!newMode)
            CleanPests();
        else
            _data.SetNewTime(_spawnTime.RandomValue);
    }

    public void CleanPests() {
        foreach (var pest in _pests)
            _pool.PutObject(pest);
        _pests.Clear();
        _data.CleanPests();

        PestsChanged?.Invoke();
        PestsCleaned?.Invoke();
    }

    public IEnumerable<Pest> Pests() {
        foreach (var pest in _pests) {
            yield return pest;
        }
    }

    public void RemovePest(Pest pest) {
        _pool.RemovePest(pest);
        _data.RemovePest(pest.Data);
        _pests.Remove(pest);
        CheckSlowdown();
    }

    private void CheckSlowdown() {
        if (_pests.Count == _maxPests)
            _data.SetMaxSlowdown();
        else
            _data.SetRegularSlowdown(_onePestSlowdown);
        PestsChanged?.Invoke();
    }

    public void Bind(PestsGeneratorData data) {
        _data = data;
        _data.SetMaxPestsCount(_maxPests);

        foreach (var pestData in _data.Pests) {
            Pest pest = _pool.GetObject(pestData.PrefabIndex);
            _pests.Add(pest);
            pest.Bind(pestData);
        }
    }
}
