using UnityEngine;

public class UIHoverListener : MonoBehaviour {
    [SerializeField] private UIHoverListener _globalListener;
    [SerializeField] private bool _isHover;

    public bool IsHover {
        get {
            if (_globalListener != null)
                return _globalListener.IsHover || _isHover;
            return _isHover;
        }
    }

    public void HoverChange(bool newValue) {
        _isHover = newValue;
    }
}
