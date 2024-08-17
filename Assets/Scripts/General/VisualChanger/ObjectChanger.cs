using UnityEngine;

public class ObjectChanger : VisualChanger {
    [SerializeField] private GameObject _disabledObject;
    [SerializeField] private GameObject _activeObject;

    protected override void UpdateVisual() {
        _disabledObject.SetActive(!_isActive);
        _activeObject.SetActive(_isActive);
    }
}
