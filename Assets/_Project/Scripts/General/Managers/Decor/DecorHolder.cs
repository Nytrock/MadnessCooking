using UnityEngine;

public class DecorHolder : MonoBehaviour {
    [SerializeField] private Decor _decor;
    [SerializeField] private VisualChanger _changer;

    public Decor Decor => _decor;

    public virtual void ChangeState(bool newValue) {
        _changer.ChangeState(newValue);
    }

    public bool CheckLocation(Location location) {
        return location == _decor.Location;
    }
}
