using System;
using UnityEngine;

[Serializable]
public struct SerializableVector
{
    [SerializeField] private float _x;
    [SerializeField] private float _y;
    [SerializeField] private float _z;

    public SerializableVector(Vector3 vector)
    {
        _x = vector.x; 
        _y = vector.y;
        _z = vector.z;
    }

    public SerializableVector(float x, float y, float z = 0)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    public Vector3 GetVector()
    {
        return new Vector3(_x, _y, _z);
    }
}
