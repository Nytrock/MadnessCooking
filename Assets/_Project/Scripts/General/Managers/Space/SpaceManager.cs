using System;
using UnityEngine;

public abstract class SpaceManager : MonoBehaviour {
    [SerializeField] protected SpacePrefab _spacePrefab;
    protected Transform _spaceContainer;

    public event Action SpaceAdded;

    public float SpaceSize => _spacePrefab.Size;

    protected virtual void Awake() {
        _spaceContainer = transform;
    }

    protected void InvokeSpaceAdded() {
        SpaceAdded?.Invoke();
    }

    protected abstract void AddSpace(int index);
    public abstract float GetSpacesSize();
}
