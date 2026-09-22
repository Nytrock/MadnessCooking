using System;
using System.Collections.Generic;
using UnityEngine;

namespace MadnessCooking.General {
    public abstract class IngredientStorage : MonoBehaviour {
        [SerializeField] protected IngredientCountList _defaultIngredients;
        [SerializeField] protected int _defaultMaxSpace = 100;

        public IngredientStorageData Data { get; protected set; }

        public event Action<IngredientCount> IngredientAdded;
        public event Action<Ingredient> IngredientRemoved;
        public event Action<IngredientCount> IngredientCountAdded;
        public event Action<IngredientCount> IngredientCountRemoved;
        public event Action LoadingDataEnded;



        public void PutIngredients(IEnumerable<IngredientCount> puttingCountList) {
            foreach (var count in puttingCountList)
                PutIngredientWithRemain(count.Ingredient, count.Count);
        }

        public virtual int PutIngredientWithRemain(Ingredient ingredient, int count) {
            if (count == 0)
                return count;

            int remainCount = 0;
            IngredientCount puttingCount = new(ingredient, count);

            if (!Data.CanAddCount(count)) {
                remainCount = Data.NowSpace + count - Data.MaxSpace;
                puttingCount = new(ingredient, Data.LeftSpace);
            }

            bool containsIngredient = Data.ContainsIngredient(puttingCount.Ingredient);
            Data.AddIngredientCount(puttingCount);
            if (!containsIngredient)
                InvokeIngredientAdded(puttingCount);
            else
                IngredientCountAdded?.Invoke(puttingCount);

            return remainCount;
        }

        public virtual void RemoveIngredients(IEnumerable<IngredientCount> ingredients) {
            foreach (var count in ingredients)
                RemoveIngredient(count);
        }

        public void RemoveIngredient(Ingredient ingredient, int count) {
            if (count == 0)
                return;

            IngredientCount removingCount = new(ingredient, count);
            RemoveIngredient(removingCount);
        }

        public void RemoveIngredient(IngredientCount removingCount) {
            Data.RemoveIngredient(removingCount);

            if (Data.GetIngredientCount(removingCount.Ingredient) == 0)
                IngredientRemoved?.Invoke(removingCount.Ingredient);
            IngredientCountRemoved?.Invoke(removingCount);
        }

        public bool HaveCount(IngredientCount count) {
            return Data.ContainsCount(count);
        }

        protected void InvokeIngredientAdded(IngredientCount count) {
            IngredientAdded?.Invoke(count);
            IngredientCountAdded?.Invoke(count);
        }

        protected void InvokeLoadingDataEnded() {
            LoadingDataEnded?.Invoke();
        }
    }
}
