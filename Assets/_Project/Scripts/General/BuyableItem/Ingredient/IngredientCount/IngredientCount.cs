using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class IngredientCount {

        [SerializeField, JsonProperty] private Ingredient _item;
        [SerializeField, Min(1), JsonProperty] private int _count;

        public Ingredient Ingredient => _item;
        public int Count => _count;

        public event Action<int> CountChanged;

        [JsonConstructor]
        public IngredientCount(Ingredient item, int count) {
            _item = item;
            _count = count;
        }

        public IngredientCount(IngredientCount buyableItemCount) {
            _item = buyableItemCount._item;
            _count = buyableItemCount._count;
        }

        public void AddToCount(int count) {
            if (_count + count < 0)
                _count = 0;
            else
                _count += count;
            CountChanged?.Invoke(_count);
        }
    }
}
