using UnityEngine.UI;

namespace MadnessCooking.General {
    public class NormalizedImage : Image {
        protected const float _sizeCoefficient = 1.079f;

        public override void SetNativeSize() {
            base.SetNativeSize();
            rectTransform.sizeDelta *= _sizeCoefficient;
        }
    }
}
