using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class BuyableItemRendererWithName : BuyableItemRenderer {
        [SerializeField] protected BuyableItemText _nameText;

        public override void SetItemInfo(BuyableItem item) {
            base.SetItemInfo(item);
            SetName(item, false);
        }

        public override void SetHiddenItemInfo(BuyableItem item) {
            base.SetHiddenItemInfo(item);
            SetName(item, true);
        }

        public override void ResetInfo() {
            base.ResetInfo();

            if (_nameText == null)
                return;

            _nameText.ResetItem();
        }

        private void SetName(BuyableItem item, bool isHidden) {
            if (_nameText == null)
                return;

            _nameText.SetType(BuyableItemTextType.Name);
            _nameText.ChangeHiddenState(isHidden);
            _nameText.SetItem(item);
        }
    }
}
