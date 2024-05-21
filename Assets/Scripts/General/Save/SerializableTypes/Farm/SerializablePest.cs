using System;

[Serializable]
public class SerializablePest
{
    public int PrefabId;
    public int SpriteId;
    public SerializableQuaternion RotationDegree;
    public SerializableVector Position;
    public SerializableVector NormalizedPosition;
}
