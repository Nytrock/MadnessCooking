using System;
using UnityEngine;
using UnityEngine.UI;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class ChickensUI : MonoBehaviour, IStateable {
        [SerializeField] private BarnChickens _chickens;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _feedButton;
        [SerializeField] private ChickensUIText _buttonText;
        [SerializeField] private Slider _eggSlider;
        [SerializeField] private ItemInfoRendererWithCount _eggRenderer;

        public event Action<bool> StateChanged;

        private void Awake() {
            _chickens.FoodCountChanged += UpdateFoodCount;
            _chickens.EggCountChanged += UpdateEggCount;
            _chickens.FeedStateChanged += UpdateSlider;
        }

        private void Start() {
            _eggRenderer.SetItemInfo(ConstIngredients.Instance.Egg);
            _eggSlider.maxValue = _chickens.EggTime;
            _panel.SetActive(false);
        }

        private void Update() {
            _eggSlider.value = _chickens.Data.NowTime;
        }

        public void Feed() => _chickens.Feed();

        private void UpdateSlider() {
            _eggSlider.gameObject.SetActive(_chickens.Data.IsFeed);
        }

        private void UpdateFoodCount() {
            ChickensData data = _chickens.Data;
            _feedButton.interactable = (data.FoodCount > 0 || data.IsInfiniteFood) && data.IsUnlocked;

            if (_chickens.Data.IsInfiniteFood)
                _buttonText.SetInfiniteText();
            else
                _buttonText.SetNormalText(_chickens.Data.FoodCount);
        }

        private void UpdateEggCount() {
            _eggRenderer.SetCount(_chickens.Data.EggCount);
        }

        public void ChangeState(bool newState) {
            _panel.SetActive(newState);
            StateChanged?.Invoke(newState);
        }

        public void EggsToCar() => _chickens.EggsToCar();
    }
}
