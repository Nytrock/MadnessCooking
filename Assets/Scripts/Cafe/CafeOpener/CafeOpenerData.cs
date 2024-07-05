using System;
using UnityEngine;

[Serializable]
public class CafeOpenerData {
    [SerializeField] private bool _isOpened = true;

    public bool IsOpened => _isOpened;

    public void ChangeCafeState() {
        _isOpened = !_isOpened;
    }
}
