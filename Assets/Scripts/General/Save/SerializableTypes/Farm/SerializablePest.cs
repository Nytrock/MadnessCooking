using System;

[Serializable]
public class SerializablePest
{
    public int PrefabIndex;
    public int SpriteIndex;
    public SerializableQuaternion RotationDegree;
    public SerializableVector Position;
    public SerializableVector NormalizedPosition;
}
