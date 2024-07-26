using System;
using UnityEngine;

public abstract class LocalDataLoader<TData> : MonoBehaviour
    where TData : ISaveable {

    protected MonoBehaviour[] _bindableObjects;
    protected IBindable<TData>[] _bindables;

    public void LoadData(TData data, bool isFileEmpty) {
        if (_bindables == null)
            GetBindables();

        foreach (var bindable in _bindables)
            bindable.Bind(data, isFileEmpty);
    }

    protected void GetBindables() {
        SetBindableObjects();
        _bindables = new IBindable<TData>[_bindableObjects.Length];
        for (int i = 0; i < _bindableObjects.Length; i++) {
            _bindables[i] = _bindableObjects[i].GetComponent<IBindable<TData>>();
            if (_bindables[i] == null)
                throw new NullReferenceException($"Object {_bindableObjects[i].name} don't have type {typeof(TData)}");
        }
    }

    protected abstract void SetBindableObjects();
}
