using System;
using UnityEngine;

[Serializable]
public class OfficeBedData {
    [SerializeField] private bool _isSleeping;

    public bool IsSleeping => _isSleeping;

    public void ChangeSleepState() {
        _isSleeping = !_isSleeping;
    }
}
