using System;
using UnityEngine;

[Serializable]
public struct SerializableQuaternion
{
    [SerializeField] private float _x;
    [SerializeField] private float _y;
    [SerializeField] private float _z;

    public SerializableQuaternion(float x, float y, float z)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    public SerializableQuaternion(Quaternion quaternion)
    {
        _x = quaternion.eulerAngles.x;
        _y = quaternion.eulerAngles.y;
        _z = quaternion.eulerAngles.z;
    }

    public Quaternion GetQuaternion()
    {
        return Quaternion.Euler(_x, _y, _z);
    }
}
