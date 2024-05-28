using System;
using UnityEngine;

public class SaveKitchenManager : SavePartManager<KitchenData> {
    [SerializeField, RequireInterface(typeof(IBindable<KitchenData>))]
    private MonoBehaviour[] _bindableObjects;

    protected override void GetBindables() {
        _bindables = new IBindable<KitchenData>[_bindableObjects.Length];
        for (int i = 0; i < _bindableObjects.Length; i++) {
            _bindables[i] = _bindableObjects[i].GetComponent<IBindable<KitchenData>>();
            if (_bindables[i] == null)
                throw new NullReferenceException($"Object {i} don't have type {typeof(KitchenData)}");
        }
    }
}
