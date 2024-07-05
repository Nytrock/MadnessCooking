using System;
using UnityEngine;

[Serializable]
public class CriticSpawnerData {
    [SerializeField] bool _isWaitingCritic;

    public bool IsWaitingCritic => _isWaitingCritic;

    public void ChangeCriticWait(bool newValue) {
        _isWaitingCritic = newValue;
    }
}
