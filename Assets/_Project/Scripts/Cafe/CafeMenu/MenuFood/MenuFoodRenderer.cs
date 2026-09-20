using System;
using UnityEngine;
using UnityEngine.UI;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    [RequireComponent(typeof(Button))]
    public class MenuFoodRenderer : MonoBehaviour {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _banishedCross;

        private MenuFood _menuFood;
        private Button _button;

        public event Action<bool> BanishedStateChanged;

        private void Awake() {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ChangeBanishedState);
        }

        public void SetMenuFood(MenuFood menuFood) {
            _menuFood = menuFood;
            _icon.sprite = _menuFood.Food.Icon;
            UpdateBanishedState();
        }

        private void ChangeBanishedState() {
            _menuFood.ChangeBanishedState();
            UpdateBanishedState();
        }

        private void UpdateBanishedState() {
            if (_banishedCross.activeSelf == _menuFood.IsBanished)
                return;

            _banishedCross.SetActive(_menuFood.IsBanished);
            BanishedStateChanged?.Invoke(_menuFood.IsBanished);
        }
    }
}
