using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class BuyableItemRendererWithDescription : BuyableItemRendererWithName {
        [SerializeField] private BuyableItemText _descriptionText;

        public override void SetItemInfo(BuyableItem item) {
            base.SetItemInfo(item);
            SetDescription(item, false);
        }

        public override void SetHiddenItemInfo(BuyableItem item) {
            base.SetHiddenItemInfo(item);
            SetDescription(item, true);
        }

        public override void ResetInfo() {
            base.ResetInfo();

            if (_descriptionText == null)
                return;

            _descriptionText.ResetItem();
        }

        private void SetDescription(BuyableItem item, bool isHidden) {
            if (_descriptionText == null)
                return;

            _descriptionText.SetType(BuyableItemTextType.Desctiption);
            _descriptionText.ChangeHiddenState(isHidden);
            _descriptionText.SetItem(item);
        }
    }
}
