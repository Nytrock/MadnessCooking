using UnityEngine;

namespace MadnessCooking.General {
    public class MenuButtonInstantSelector : MenuButtonSelector {
        protected override void ChangePosition() {
            _nowRect.position = new Vector2(_nowRect.position.x, _targetRect.position.y);
            _nowRect.sizeDelta = new Vector2(_targetRect.sizeDelta.x, _nowRect.sizeDelta.y);
        }
    }
}
