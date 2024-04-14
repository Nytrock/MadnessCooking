using System;
using UnityEngine;

public class SaveMainManager : SavePartManager<MainData>
{
    [SerializeField, RequireInterface(typeof(IBindable<MainData>))]
    private MonoBehaviour[] _bindableObjects;

    protected override void GetBindables()
    {
        _bindables = new IBindable<MainData>[_bindableObjects.Length];
        for (int i = 0; i < _bindableObjects.Length; i++) {
            _bindables[i] = _bindableObjects[i].GetComponent<IBindable<MainData>>();
            if (_bindables[i] == null) {
                throw new NullReferenceException($"Object {i} don't have type {typeof(MainData)}");
            }
        }
    }
}
