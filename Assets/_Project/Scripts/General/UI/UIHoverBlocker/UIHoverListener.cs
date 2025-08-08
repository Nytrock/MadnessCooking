using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverListener : MonoBehaviour {
    [SerializeField] private UIHoverListener _globalListener;
    [SerializeField] private bool _isScrollBlocked;

    public bool IsHover {
        get {
            PointerEventData eventData = new(EventSystem.current) {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
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
