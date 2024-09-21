using UnityEngine;

public class DecorObjectChanger : DecorHolder {
    [SerializeField] private GameObject _noDecorObject;
    [SerializeField] private GameObject _haveDecorObject;

    public override void ChangeState(bool newValue) {
        _noDecorObject.SetActive(!newValue);
        _haveDecorObject.SetActive(newValue);
    }
}
