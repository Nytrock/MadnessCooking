using MadnessCooking.General;
using System;
using UnityEngine;

namespace MadnessCooking.Farm {
    public class BarnStorage : IngredientStorage {
        [SerializeField] private FarmCar _car;
        [SerializeField] private Cow _cow;
        [SerializeField] private FlourMill _flourMill;
        private int _milkCount = 0;

        private Ingredient Milk => ConstIngredients.Instance.Milk;
        private Ingredient Flour => ConstIngredients.Instance.Flour;

        public event Action<int> MilkCountUpdated;
        public event Action<int> FlourCountUpdated;

        private void Awake() {
            Data = new();
            Data.SetMaxSpace(-1);

            _cow.ReadyCountChanged += UpdateMilkCount;
            _flourMill.ReadyCountChanged += UpdateFlourCount;
        }

        private void UpdateMilkCount() {
            int difference = _cow.Data.ReadyCount - _milkCount;

            if (difference > 0)
                PutIngredientWithRemain(Milk, difference);
            else if (difference < 0)
                RemoveIngredient(Milk, -difference);

            _milkCount = _cow.Data.ReadyCount;
            MilkCountUpdated?.Invoke(_cow.Data.ReadyCount);
        }

        private void UpdateFlourCount() {
            FlourCountUpdated?.Invoke(_flourMill.Data.ReadyCount);
        }

        public void PutIngredient(Ingredient ingredient) {
            if (ingredient != Milk && ingredient != Flour)
                throw new ArgumentException("Unknown ingredient");

            if (_car.Data.LeftSpace == 0)
                return;

            MoveToCar(ingredient);
            UpdateFlourCount();
        }

        private void MoveToCar(Ingredient ingredient) {
            NeedHoldAdd changingHoldAdd;
            if (ingredient == Milk)
                changingHoldAdd = _cow;
            else
                changingHoldAdd = _flourMill;

            int oldCount = changingHoldAdd.Data.ReadyCount;
            if (oldCount == 0)
                return;

            int remainCount = _car.PutIngredientWithRemain(ingredient, changingHoldAdd.Data.ReadyCount);
            FatigueManager.Instance.AddFatigue(ingredient.FatigueCoef * (changingHoldAdd.Data.ReadyCount - remainCount));
            changingHoldAdd.SetReady(remainCount);

            if (ingredient == Milk)
                RemoveIngredient(ingredient, oldCount - remainCount);
        }
    }
}
