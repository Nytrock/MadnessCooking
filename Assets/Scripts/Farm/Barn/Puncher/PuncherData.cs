using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class PuncherData {
    [SerializeField, JsonProperty] private float _nowWaste;
    [SerializeField, JsonProperty] private int _fertilizerCount;
    [SerializeField, JsonProperty] private float _speed;
    [SerializeField] private float _needWaste;

    public int FertilizerCount => _fertilizerCount;
    public float NowWaste => _nowWaste;
    public float NeedWaste => _needWaste;

    public PuncherData() {
        _nowWaste = 0;
        _speed = 0;
        _fertilizerCount = 0;
    }

    public void SetNeedWaste(float needWaste) {
        _needWaste = needWaste;
    }

    public void AddWaste(float wasteAmount) {
        _nowWaste += wasteAmount * _speed;
        while (_nowWaste > _needWaste) {
            _nowWaste -= _needWaste;
            _fertilizerCount++;
        }
    }

    public void SubtractFertilizer() {
        if (_fertilizerCount == 0)
            return;

        _fertilizerCount--;
    }

    public void ChangeSpeed(CoefficientUpgrade upgrade) {
        _speed = upgrade.Coefficient;
    }
}
