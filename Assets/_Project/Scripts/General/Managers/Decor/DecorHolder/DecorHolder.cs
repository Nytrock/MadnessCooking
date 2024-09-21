using UnityEngine;

public class DecorHolder : MonoBehaviour {
    [SerializeField] private Decor _decor;

    public Decor Decor => _decor;

    public virtual void ChangeState(bool newValue) {
        gameObject.SetActive(newValue);
    }

    public bool CheckLocation(Location location) {
        return location == _decor.Location;
    }
}
