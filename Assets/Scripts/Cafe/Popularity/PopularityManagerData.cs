using System;
using UnityEngine;

[Serializable]
public class PopularityManagerData {
    [SerializeField] private int _level;
    [SerializeField] private int _xp;
    [SerializeField] private bool _isMaxLevel;

    public int Level => _level;
    public int Xp => _xp;
    public bool IsMaxLevel => _isMaxLevel;

    public PopularityManagerData() {
        _level = 0;
        _xp = 0;
    }

    public void AddXp(int xp) {
        _xp += xp;
    }

    public void RemoveXp(int xp) {
        _xp -= xp;
    }

    public void NextLevel(int levelsCount) {
        _level++;
        if (levelsCount == _level + 1)
            _isMaxLevel = true;
    }

    public void PreviousLevel() {
        if (_level == 0)
            return;

        _level--;
        _isMaxLevel = false;
    }
}
