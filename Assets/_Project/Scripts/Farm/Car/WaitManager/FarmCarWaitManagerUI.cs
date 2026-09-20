using System.Collections.Generic;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class FarmCarWaitManagerUI : MonoBehaviour {
        [SerializeField] private FarmCarWaitManager _waitManager;
        [SerializeField] private GameObject _panel;
        [SerializeField] private IngredientCountButtonPool _buttonsPool;

        private readonly List<IngredientCountButton> _buttons = new();

        private void Awake() {
            _waitManager.StateChanged += UpdateItemsList;
        }

        public void ChangeState(bool newState) {
            _panel.SetActive(newState && _waitManager.CarState == CarState.Sent);
        }

        private void UpdateItemsList(CarState state) {
            switch (state) {
                case CarState.Sent:
                    CreateList();
                    break;
                case CarState.Returns:
                    ChangeState(false);
                    DestroyList();
                    break;
                default: break;
            }
        }

        private void CreateList() {
            foreach (var ingredientCount in _waitManager.IngredientsSended)
                CreateButton(ingredientCount);
        }

        private void CreateButton(IngredientCount ingredientCount) {
            IngredientCountButton button = _buttonsPool.GetObject(ingredientCount);
            _buttons.Add(button);
        }

        private void DestroyList() {
            foreach (var button in _buttons)
                DestroyButton(button);
            _buttons.Clear();
        }

        private void DestroyButton(IngredientCountButton button) {
            _buttonsPool.PutObject(button);
        }

    }
}
