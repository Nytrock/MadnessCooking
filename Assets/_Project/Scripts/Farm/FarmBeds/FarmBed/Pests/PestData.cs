using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class PestData {
    [SerializeField, JsonProperty] private int _prefabIndex;
    [SerializeField, JsonProperty] private int _spriteIndex;
    [SerializeField, JsonProperty] private Quaternion _rotationDegree;
    [SerializeField, JsonProperty] private Vector2 _position;
    [SerializeField, JsonProperty] private Vector2 _normalizedPosition;

    public PestData(int prefabIndex, int spriteIndex, Quaternion rotation,
        Vector2 position, Vector2 normalizedPosition) {
        _prefabIndex = prefabIndex;
        _spriteIndex = spriteIndex;
        _rotationDegree = rotation;
        _position = position;
        _normalizedPosition = normalizedPosition;
    }

    public int PrefabIndex => _prefabIndex;
    public int SpriteIndex => _spriteIndex;
    public Quaternion RotationDegree => _rotationDegree;
    public Vector2 Position => _position;
    public Vector2 NormalizedPosition => _normalizedPosition;
}
