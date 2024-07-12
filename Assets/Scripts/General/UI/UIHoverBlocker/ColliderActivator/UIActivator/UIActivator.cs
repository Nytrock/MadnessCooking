using UnityEngine;

public class UIActivator : ColliderActivator {
    [SerializeField, RequireInterface(typeof(IActivable))]
    private MonoBehaviour _activableObject;
    [SerializeField] private LocationManager _locationManager;

    private IActivable _activable;

    protected override void Awake() {
        base.Awake();
        _activable = _activableObject.GetComponent<IActivable>();
        _locationManager.LocationChanged += delegate { CloseUI(); };
    }

    protected override void Press() {
        _activable.ChangeState();
    }

    protected void CloseUI() {
        _activable.ChangeState(false);
    }
}
