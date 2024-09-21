using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class PestData {
    [SerializeField, JsonProperty] private int _prefabIndex;
    [SerializeField, JsonProperty] private int _spriteIndex;
    [SerializeField, JsonProperty] private JsonQuaternion _rotationDegree;
    [SerializeField, JsonProperty] private JsonVector _position;
    [SerializeField, JsonProperty] private JsonVector _normalizedPosition;

    public PestData(int prefabIndex, int spriteIndex, JsonQuaternion rotation,
        JsonVector position, JsonVector normalizedPosition) {
        _prefabIndex = prefabIndex;
        _spriteIndex = spriteIndex;
        _rotationDegree = rotation;
        _position = position;
        _normalizedPosition = normalizedPosition;
    }

    public int PrefabIndex => _prefabIndex;
    public int SpriteIndex => _spriteIndex;
    public Quaternion RotationDegree => _rotationDegree.GetQuaternion();
    public Vector2 Position => _position.GetVector();
    public Vector2 NormalizedPosition => _normalizedPosition.GetVector();
}
