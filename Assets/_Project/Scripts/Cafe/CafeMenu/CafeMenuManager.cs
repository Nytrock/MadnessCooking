using MadnessCooking.General;
using MadnessCooking.Kitchen;
using UnityEngine;

namespace MadnessCooking.Cafe {
    public class CafeMenuManager : MonoBehaviour {
        [SerializeField] private CafeStateChanger _cafeOpener;
        [SerializeField] private CafeMenuRenderer _renderer;
        [SerializeField] private ErrorMessageRenderer _errorMessage;
        [SerializeField] private FoodManager _foodManager;
        [SerializeField] private GameSaveManager _saveManager;

        private void Awake() {
            _foodManager.MenuFoodAdded += AddMenuFood;
            _saveManager.AfterLateStart += GenerateFoodMenu;
        }

        private void GenerateFoodMenu() {
            foreach (var menuFood in _foodManager.FoodMenu)
                AddMenuFood(menuFood);
        }

        private void AddMenuFood(MenuFood menuFood) {
            _renderer.AddMenuFood(menuFood);
        }

        public void OpenMenu() {
            if (_cafeOpener.IsOpened) {
                _errorMessage.ShowError();
                return;
            }

            _renderer.ChangeState(true);
        }
    }
}
