using System;
using UnityEngine;

[Serializable]
public struct SerializableQuaternion
{
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;

    public SerializableQuaternion(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public SerializableQuaternion(Quaternion quaternion)
    {
        x = quaternion.eulerAngles.x;
        y = quaternion.eulerAngles.y;
        z = quaternion.eulerAngles.z;
    }

    public Quaternion GetQuaternion()
    {
        return Quaternion.Euler(x, y, z);
    }
}
