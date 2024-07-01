using System;
using UnityEngine;

[Serializable]
public class AutoSaveManagerData {
    [SerializeField] private float _autoSaveNowTime = 0;

    public float AutoSaveNowTime => _autoSaveNowTime;

    public void AddTime() {
        _autoSaveNowTime += Time.deltaTime;
    }

    public void ResetTime() {
        _autoSaveNowTime = 0;
    }
}
