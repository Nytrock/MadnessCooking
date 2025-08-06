using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverListener : MonoBehaviour {
    [SerializeField] private UIHoverListener _globalListener;
    [SerializeField] private bool _isScrollBlocked;

    public bool IsHover => EventSystem.current.IsPointerOverGameObject();

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
