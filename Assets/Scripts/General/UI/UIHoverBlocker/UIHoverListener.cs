using System;
using UnityEngine;

public class UIHoverListener : MonoBehaviour {
    [field: SerializeField] private bool _isHover;

    public event Action<bool> OnHover;

    public void HoverChange(bool newValue) {
        _isHover = newValue;
        OnHover?.Invoke(_isHover);
    }
}
