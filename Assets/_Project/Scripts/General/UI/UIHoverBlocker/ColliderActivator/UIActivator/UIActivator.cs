using AYellowpaper;
using System;
using UnityEngine;


public class UIActivator : ColliderActivator {
    [SerializeField, RequireInterface(typeof(IActivable))]
    protected MonoBehaviour _activableObject;
    [SerializeField] private LocationManager _locationManager;

    protected IActivable _activable;
    private bool _isActive;

    public event Action<bool> StateChanged;

    protected virtual void Awake() {
        _activable = _activableObject.GetComponent<IActivable>();

        if (_locationManager != null)
            _locationManager.LocationChanged += delegate { CloseUI(); };
    }

    protected override void Press() {
        _activable.ChangeState();

        _isActive = !_isActive;
        StateChanged?.Invoke(_isActive);
    }

    protected virtual void CloseUI() {
        _activable.ChangeState(false);
        _isActive = false;
    }
}
