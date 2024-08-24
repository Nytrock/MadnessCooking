using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public struct JsonVector {
    [SerializeField, JsonProperty] private float _x;
    [SerializeField, JsonProperty] private float _y;
    [SerializeField, JsonProperty] private float _z;

    public JsonVector(Vector3 vector) {
        _x = vector.x;
        _y = vector.y;
        _z = vector.z;
    }

    public JsonVector(float x, float y, float z = 0) {
        _x = x;
        _y = y;
        _z = z;
    }

    public Vector3 GetVector() {
        return new Vector3(_x, _y, _z);
    }
}
