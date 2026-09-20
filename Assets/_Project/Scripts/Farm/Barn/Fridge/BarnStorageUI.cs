using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class BarnStorageUI : MonoBehaviour, IStateable {
        [SerializeField] private GameObject _panel;
        [SerializeField] private BarnStorage _barnStorage;
        [SerializeField] private ItemInfoRendererWithCount _milkRenderer;
        [SerializeField] private ItemInfoRendererWithCount _flourRenderer;

        public event Action<bool> StateChanged;

        private void Awake() {
            _barnStorage.MilkCountUpdated += UpdateMilkCount;
            _barnStorage.FlourCountUpdated += UpdateFlourCount;
        }

        private void UpdateMilkCount(int milkCount) {
            _milkRenderer.SetCount(milkCount);
        }

        private void UpdateFlourCount(int flourCount) {
            _flourRenderer.SetCount(flourCount);
        }

        private void Start() {
            _panel.SetActive(false);
            _milkRenderer.SetItemInfo(ConstIngredients.Instance.Milk);
            _flourRenderer.SetItemInfo(ConstIngredients.Instance.Flour);
        }

        public void ChangeState(bool newState) {
            _panel.SetActive(newState);
            StateChanged?.Invoke(newState);
        }
    }
}
