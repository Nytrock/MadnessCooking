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

    public Vector3 GetVector()
    {
        return new Vector3(x, y, z);
    }
}
