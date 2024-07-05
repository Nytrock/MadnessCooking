using System;
using UnityEngine;

[Serializable]
public class PestData {
    [SerializeField] private int _prefabIndex;
    [SerializeField] private int _spriteIndex;
    [SerializeField] private SerializableQuaternion _rotationDegree;
    [SerializeField] private SerializableVector _position;
    [SerializeField] private SerializableVector _normalizedPosition;

    public PestData(int prefabIndex, int spriteIndex, SerializableQuaternion rotation,
        SerializableVector position, SerializableVector normalizedPosition) {
        _prefabIndex = prefabIndex;
        _spriteIndex = spriteIndex;
        _rotationDegree = rotation;
        _position = position;
        _normalizedPosition = normalizedPosition;
    }

    public int PrefabIndex => _prefabIndex;
    public int SpriteIndex => _spriteIndex;
    public SerializableQuaternion RotationDegree => _rotationDegree;
    public SerializableVector Position => _position;
    public SerializableVector NormalizedPosition => _normalizedPosition;
}
