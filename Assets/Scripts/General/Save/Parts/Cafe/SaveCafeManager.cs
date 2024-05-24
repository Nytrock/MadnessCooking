using System;
using UnityEngine;

public class SaveCafeManager : SavePartManager<CafeData>
{
    [SerializeField, RequireInterface(typeof(IBindable<CafeData>))]
    private MonoBehaviour[] _bindableObjects;

    protected override void GetBindables()
    {
        _bindables = new IBindable<CafeData>[_bindableObjects.Length];
        for (int i = 0; i < _bindableObjects.Length; i++) {
            _bindables[i] = _bindableObjects[i].GetComponent<IBindable<CafeData>>();
            if (_bindables[i] == null)
                throw new NullReferenceException($"Object {i} don't have type {typeof(CafeData)}");
        }
    }
}
