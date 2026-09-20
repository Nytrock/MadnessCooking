using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class CafeSeat : MonoBehaviour {
        [SerializeField] private Direction _seatDirection;
        [SerializeField] private TableFoodRenderer _foodRenderer;

        public Direction SeatDirection => _seatDirection;

        public event Action<bool> StateChanged;

        public void ChangeSeatState(bool newState) {
            StateChanged?.Invoke(newState);
        }

        public void SetTableFoodSprite(Food food) {
            _foodRenderer.ShowFood(food);
        }

        public void ResetTableFoodSprite() {
            _foodRenderer.HideFood();
        }
    }
}
