using UnityEngine;
using UnityEngine.UI;

namespace MadnessCooking.General {
    public class ImageWithHole : Image {
        [SerializeField] private RectTransform _hole;

        public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera) {
            if (_hole.ContainsMouse())
                return false;
            return base.IsRaycastLocationValid(screenPoint, eventCamera);
        }
    }
}