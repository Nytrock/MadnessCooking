using System;
using UnityEngine;

public class SaveOfficeManager : SavePartManager<OfficeData> {
    [SerializeField, RequireInterface(typeof(IBindable<OfficeData>))]
    private MonoBehaviour[] _bindableObjects;

    protected override void GetBindables() {
        _bindables = new IBindable<OfficeData>[_bindableObjects.Length];
        for (int i = 0; i < _bindableObjects.Length; i++) {
            _bindables[i] = _bindableObjects[i].GetComponent<IBindable<OfficeData>>();
            if (_bindables[i] == null)
                throw new NullReferenceException($"Object {i} don't have type {typeof(OfficeData)}");
        }
    }
}
