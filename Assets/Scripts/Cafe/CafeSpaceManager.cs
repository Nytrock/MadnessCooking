using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeSpaceManager : MonoBehaviour
{
    [SerializeField] private int _spaceCount = 2;
    [SerializeField] private CafeSpace _spacePrefab;
    
    private Transform _spaceContainer;
    public int SpaceCount => _spaceCount;
    public event Action SpaceAdded;

    private void Start()
    {
        _spaceContainer = transform;
        SetSpace();
    }

    private void SetSpace()
    {
        var size = GetSpaceSize();

        for (int i = 0; i < _spaceCount; i++) {
            var space = Instantiate(_spacePrefab);
            space.transform.parent = _spaceContainer;
            space.transform.position += new Vector3(size * i, 0, 0);
        }
    }
    public float GetSpaceSize()
    {
        return _spacePrefab.transform.localScale.x;
    }

    private void AddSpace()
    {
        var space = Instantiate(_spacePrefab);
        space.transform.parent = _spaceContainer;
        space.transform.position += new Vector3(GetSpaceSize() * _spaceCount, 0, 0);
        SpaceAdded?.Invoke();
    }
}