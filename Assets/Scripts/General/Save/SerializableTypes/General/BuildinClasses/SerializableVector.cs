using System;
using UnityEngine;

[Serializable]
public struct SerializableVector
{
    public float x;
    public float y;
    public float z;

    public SerializableVector(Vector3 vector)
    {
        x = vector.x; 
        y = vector.y;
        z = vector.z;
    }

    public SerializableVector(float x, float y, float z = 0)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 GetVector()
    {
        return new Vector3(x, y, z);
    }
}
