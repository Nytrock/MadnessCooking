using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class ItemInfoRendererWithCount : BuyableItemRenderer {
        [SerializeField] private CountRenderer _count;

        public void SetCount(int count) {
            _count.UpdateCount(count);
        }

        public override void ResetInfo() {
            base.ResetInfo();
            _count.ResetText();
        }
    }
}
