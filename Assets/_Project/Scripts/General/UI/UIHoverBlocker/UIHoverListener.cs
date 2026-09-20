using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MadnessCooking.General {
    public class UIHoverListener : MonoBehaviour {
        [SerializeField] private UIHoverListener _globalListener;
        [SerializeField] private bool _isScrollBlocked;

        private readonly List<RaycastResult> _hoverResults = new();

        public bool IsHover {
            get {
                PointerEventData eventData = new(EventSystem.current) {
                    position = Input.mousePosition
                };
                EventSystem.current.RaycastAll(eventData, _hoverResults);
                return _hoverResults.Count > 0;
            }
        }

        public bool IsScrollBlocked {
            get {
                if (_globalListener != null)
                    return _globalListener.IsScrollBlocked || _isScrollBlocked;
                return _isScrollBlocked;
            }
        }

        public void ChangeScrollBlockState(bool newValue) {
            _isScrollBlocked = newValue;
        }
    }
}
