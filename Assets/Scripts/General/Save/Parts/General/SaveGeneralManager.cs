using System;
using UnityEngine;

public class SaveGeneralManager : SavePartManager<GeneralData> {
    [SerializeField, RequireInterface(typeof(IBindable<GeneralData>))]
    private MonoBehaviour[] _bindableObjects;

    protected override void GetBindables() {
        _bindables = new IBindable<GeneralData>[_bindableObjects.Length];
        for (int i = 0; i < _bindableObjects.Length; i++) {
            _bindables[i] = _bindableObjects[i].GetComponent<IBindable<GeneralData>>();
            if (_bindables[i] == null)
                throw new NullReferenceException($"Object {i} don't have type {typeof(GeneralData)}");
        }
    }
}
