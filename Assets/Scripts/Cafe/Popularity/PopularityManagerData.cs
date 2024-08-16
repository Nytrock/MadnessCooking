using System;
using UnityEngine;

[Serializable]
public class PopularityManagerData {
    [SerializeField] private int _level;
    [SerializeField] private int _xp;

    public int Level => _level;
    public int Xp => _xp;

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

    public void NextLevel() {
        _level++;
    }

    public void PreviousLevel() {
        if (_level == 0)
            return;

        _level--;
    }
}
