using UnityEngine;

public class UIHoverListener : MonoBehaviour {
    [SerializeField] private UIHoverListener _globalListener;
    [SerializeField] private bool _isHover;
    [SerializeField] private bool _isScrollBlocked;

    public bool IsHover {
        get {
            if (_globalListener != null)
                return _globalListener.IsHover || _isHover;
            return _isHover;
        }
    }

    public bool IsScrollBlocked {
        get {
            if (_globalListener != null)
                return _globalListener.IsScrollBlocked || _isScrollBlocked;
            return _isScrollBlocked;
        }
    }

    public void ChangeHoverState(bool newValue) {
        _isHover = newValue;
    }

    public void ChangeScrollBlockState(bool newValue) {
        _isScrollBlocked = newValue;
    }
}
