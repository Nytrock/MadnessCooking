using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<TObject> : MonoBehaviour
    where TObject : MonoBehaviour {

    [SerializeField] protected Transform _container;
    protected Queue<TObject> _pool = new();

    public virtual TObject GetObject() {
        if (_pool.Count == 0)
            return CreateObject();
        return _pool.Dequeue();
    }

    public virtual void PutObject(TObject obj) {
        if (_pool.Contains(obj))
            return;

        _pool.Enqueue(obj);
    }

    protected abstract TObject CreateObject();
}
