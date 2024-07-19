using System;
using UnityEngine;

[Serializable]
public class PuncherData {
    [SerializeField] private float _nowWaste;
    [SerializeField] private float _fertilizerCount;

    public float FertilizerCount => _fertilizerCount;

    public void SubtractFertilizer() {
        if (_fertilizerCount == 0)
            return;

        _fertilizerCount--;
    }
}
