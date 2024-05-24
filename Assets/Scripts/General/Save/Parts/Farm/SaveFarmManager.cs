using System;
using UnityEngine;

public class SaveFarmManager : SavePartManager<FarmData>
{
    [SerializeField, RequireInterface(typeof(IBindable<FarmData>))]
    private MonoBehaviour[] _bindableObjects;

    protected override void GetBindables()
    {
        _bindables = new IBindable<FarmData>[_bindableObjects.Length];
        for (int i = 0; i < _bindableObjects.Length; i++) {
            _bindables[i] = _bindableObjects[i].GetComponent<IBindable<FarmData>>();
            if (_bindables[i] == null)
                throw new NullReferenceException($"Object {i} don't have type {typeof(FarmData)}");
        }
    }
}
