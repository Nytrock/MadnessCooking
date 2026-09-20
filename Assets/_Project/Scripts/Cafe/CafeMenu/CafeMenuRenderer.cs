using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class CafeMenuRenderer : MonoBehaviour {
        [SerializeField] private GameObject _panel;
        [SerializeField] private RectTransform _menuFoodContainer;
        [SerializeField] private MenuFoodRenderer _menuFoodPrefab;

        public void ChangeState(bool newState) {
            _panel.SetActive(newState);
            if (newState)
                _menuFoodContainer.ForceUpdateRect();
        }

        public void AddMenuFood(MenuFood menuFood) {
            MenuFoodRenderer menuFoodRenderer = Instantiate(_menuFoodPrefab, _menuFoodContainer);
            menuFoodRenderer.SetMenuFood(menuFood);
        }
    }
}
