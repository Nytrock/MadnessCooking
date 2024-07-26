using UnityEngine;


public class UIActivator : ColliderActivator {
    [SerializeField, AYellowpaper.RequireInterface(typeof(IActivable))]
    protected MonoBehaviour _activableObject;
    [SerializeField] private LocationManager _locationManager;

    protected IActivable _activable;

    protected override void Awake() {
        base.Awake();
        _activable = _activableObject.GetComponent<IActivable>();

        if (_locationManager != null)
            _locationManager.LocationChanged += delegate { CloseUI(); };
    }

    protected override void Press() {
        _activable.ChangeState();
    }

    protected void CloseUI() {
        _activable.ChangeState(false);
    }
}
