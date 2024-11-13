using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class PestsGeneratorData {
    [SerializeField, JsonProperty] private List<PestData> _pests;
    [SerializeField, JsonProperty] private float _pestsSlowdown;
    [SerializeField, JsonProperty] private float _nowTime;
    [SerializeField, JsonProperty] private float _needTime;
    [SerializeField, JsonProperty] private bool _isActive;
    [SerializeField, JsonProperty] private bool _isPestsInstant;
    [SerializeField, JsonProperty] private bool _isPestsRemoved;

    public IEnumerable<PestData> Pests => _pests;
    public float PestsSlowdown => _pestsSlowdown;
    public float NowTime => _nowTime;
    public float NeedTime => _needTime;
    public bool IsActive => _isActive;
    public bool IsPestsInstant => _isPestsInstant;
    public bool IsPestsRemoved => _isPestsRemoved;

    public PestsGeneratorData() {
        _pests = new();
        _pestsSlowdown = 1;
    }

    public void UpdateTime() {
        _nowTime += InGameTime.Instance.NormalizedDeltaTime;
    }

    public void DisableUpgrades() {
        _isPestsInstant = false;
        _isPestsRemoved = false;
    }

    public void SetNewTime(float spawnTime) {
        _nowTime = 0;
        _needTime = spawnTime;
    }

    public void AddPest(PestData pestData) {
        _pests.Add(pestData);
    }

    public void RemovePest(PestData pestData) {
        _pests.Remove(pestData);
    }

    public void ChangeMode(bool newMode) {
        _isActive = newMode;
    }

    public void CleanPests() {
        _pests.Clear();
        _pestsSlowdown = 1;
    }

    public void SetMaxSlowdown() {
        _pestsSlowdown = 0;
        _isActive = false;
    }

    public void SetRegularSlowdown(float onePestSlowdown) {
        _isActive = true;
        _pestsSlowdown = 1 - (_pests.Count * onePestSlowdown);
    }

    public void SetInstantUpgrade() {
        _isPestsInstant = true;
    }

    public void SetRemoveUpgrade() {
        _isPestsRemoved = true;
    }
}
