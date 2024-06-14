using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<TObject> : MonoBehaviour
    where TObject : MonoBehaviour {

    [SerializeField] protected Transform _container;
    protected Queue<TObject> _pool = new();

    public abstract TObject GetObject();
    public abstract void PutObject(TObject obj);
}
