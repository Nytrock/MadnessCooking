using System;
using UnityEngine;

[Serializable]
public class PopularityManagerData {
    [SerializeField] private int _level = 0;
    [SerializeField] private int _xp = 0;
    [SerializeField] private bool _isMaxLevel;

    public int Level => _level;
    public int Xp => _xp;
    public bool IsMaxLevel => _isMaxLevel;

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
