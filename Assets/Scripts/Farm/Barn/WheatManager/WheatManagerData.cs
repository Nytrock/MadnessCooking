using System;
using UnityEngine;

[Serializable]
public class WheatManagerData {
    [SerializeField] private bool _isCowNextWheat;

    public bool IsCowNextWheat => _isCowNextWheat;

    public void ChangeCowNextWheat() {
        _isCowNextWheat = !_isCowNextWheat;
    }
}
