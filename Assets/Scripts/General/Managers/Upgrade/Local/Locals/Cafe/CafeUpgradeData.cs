using System;
using UnityEngine;

[Serializable]
public class CafeUpgradeData : ISaveable {
    [SerializeField] private bool _isEatTimeShow;

    public bool IsEatTimeShow => _isEatTimeShow;

    public void ChangeEatTimeShow(bool isEatTimeShow) {
        _isEatTimeShow = isEatTimeShow;
    }
}
