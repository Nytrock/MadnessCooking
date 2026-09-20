using UnityEngine;

namespace MadnessCooking.General {
    public class BuyPanelSideInfoData : GrayscaleImageData {
        private string _hoverText;

        public string HoverText => _hoverText;

        public BuyPanelSideInfoData(Sprite sprite, bool isGrayscale, string hoverText) : base(sprite, isGrayscale) {
            _hoverText = hoverText;
        }
    }
}
