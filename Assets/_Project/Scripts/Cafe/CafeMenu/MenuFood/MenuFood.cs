using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    [Serializable]
    public class MenuFood {
        [SerializeField] private Food _food;
        [SerializeField] private bool _isBanished;

        public Food Food => _food;
        public bool IsBanished => _isBanished;

        public event Action BanishedStateChanged;

        public MenuFood(Food food) {
            _food = food;
            _isBanished = false;
        }

        public void ChangeBanishedState() {
            _isBanished = !_isBanished;
            BanishedStateChanged?.Invoke();
        }
    }
}
