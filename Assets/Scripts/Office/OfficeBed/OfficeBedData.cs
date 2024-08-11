using System;
using UnityEngine;

[Serializable]
public class OfficeBedData {
    [SerializeField] private bool _isSleep;

    public bool IsSleep => _isSleep;

    public void ChangeSleepState() {
        _isSleep = !_isSleep;
    }
}
