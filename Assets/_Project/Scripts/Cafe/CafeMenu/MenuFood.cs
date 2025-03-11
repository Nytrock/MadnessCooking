using System;
using UnityEngine;

[Serializable]
public class MenuFood {
    [SerializeField] private Food _food;
    [SerializeField] private bool _isBanished;

    public Food Food => _food;
    public bool IsBanished => _isBanished;

    public MenuFood(Food food) {
        _food = food;
        _isBanished = false;
    }

    public void ChangeBanishedState() {
        _isBanished = !_isBanished;
    }
}
